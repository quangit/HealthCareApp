using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Graphics;
using Android.Util;

namespace HealthCare.Droid.Controls
{
    [Register("healthcare.droid.controls.ZoomImageView")]
    public class ZoomImageView : MvxImageView, View.IOnTouchListener, ScaleGestureDetector.IOnScaleGestureListener
    {
        private Matrix _matrix;

        // We can be in one of these 3 states
        private const int NONE = 0;
        private const int DRAG = 1;
        private const int ZOOM = 2;

        private static int _mode = NONE;

        // Remember some things for zooming
        private readonly PointF _last = new PointF();
        private readonly PointF _start = new PointF();
        private const float MIN_SCALE = 1f;
        private float _maxScale = 3f;
        private float[] _m;


        private int _viewWidth, _viewHeight;
        private const int CLICK = 3;
        private float _saveScale = 1f;
        protected float OrigWidth, OrigHeight;
        private int _oldMeasuredWidth, _oldMeasuredHeight;


        private ScaleGestureDetector _mScaleDetector;

        private Context _context;

        public ZoomImageView(Context context)
            : base(context)
        {

            SharedConstructing(context);
        }

        public ZoomImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

            SharedConstructing(context);
        }


        public bool OnTouch(View v, MotionEvent e)
        {
            _mScaleDetector.OnTouchEvent(e);
            var curr = new PointF(e.GetX(), e.GetY());

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    _last.Set(curr);
                    _start.Set(_last);
                    _mode = DRAG;
                    break;

                case MotionEventActions.Move:
                    if (_mode == DRAG)
                    {
                        var deltaX = curr.X - _last.X;
                        var deltaY = curr.Y - _last.Y;
                        var fixTransX = GetFixDragTrans(deltaX, _viewWidth, OrigWidth * _saveScale);
                        var fixTransY = GetFixDragTrans(deltaY, _viewHeight, OrigHeight * _saveScale);
                        _matrix.PostTranslate(fixTransX, fixTransY);
                        FixTrans();
                        _last.Set(curr.X, curr.Y);
                    }
                    break;
                case MotionEventActions.Up:
                    _mode = NONE;
                    var xDiff = (int)Math.Abs(curr.X - _start.X);
                    var yDiff = (int)Math.Abs(curr.Y - _start.Y);
                    if (xDiff < CLICK && yDiff < CLICK)
                        PerformClick();
                    break;
                case MotionEventActions.PointerUp:
                    _mode = NONE;
                    break;
            }

            ImageMatrix = (_matrix);
            Invalidate();
            return true;
        }
        private void SharedConstructing(Context context)
        {
            Clickable = true;
            _context = context;
            _mScaleDetector = new ScaleGestureDetector(context, this);
            _matrix = new Matrix();
            _m = new float[9];
            ImageMatrix = _matrix;
            SetScaleType(ScaleType.Matrix);
            SetOnTouchListener(this);
        }

        public void SetMaxZoom(float x)
        {
            _maxScale = x;
        }

        void FixTrans()
        {
            _matrix.GetValues(_m);
            var transX = _m[Matrix.MtransX];
            var transY = _m[Matrix.MtransY];

            var fixTransX = GetFixTrans(transX, _viewWidth, OrigWidth * _saveScale);
            var fixTransY = GetFixTrans(transY, _viewHeight, OrigHeight * _saveScale);

            if (fixTransX != 0 || fixTransY != 0)
                _matrix.PostTranslate(fixTransX, fixTransY);
        }

        float GetFixTrans(float trans, float viewSize, float contentSize)
        {
            float minTrans, maxTrans;

            if (contentSize <= viewSize)
            {
                minTrans = 0;
                maxTrans = viewSize - contentSize;
            }
            else
            {
                minTrans = viewSize - contentSize;
                maxTrans = 0;
            }

            if (trans < minTrans)
                return -trans + minTrans;
            if (trans > maxTrans)
                return -trans + maxTrans;
            return 0;
        }

        float GetFixDragTrans(float delta, float viewSize, float contentSize)
        {
            if (contentSize <= viewSize)
            {
                return 0;
            }
            return delta;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            _viewWidth = MeasureSpec.GetSize(widthMeasureSpec);
            _viewHeight = MeasureSpec.GetSize(heightMeasureSpec);

            //
            // Rescales image on rotation
            //
            if (_oldMeasuredHeight == _viewWidth && _oldMeasuredHeight == _viewHeight
                    || _viewWidth == 0 || _viewHeight == 0)
                return;
            _oldMeasuredHeight = _viewHeight;
            _oldMeasuredWidth = _viewWidth;

            if (_saveScale == 1)
            {
                //Fit to screen.
                float scale;

                var drawable = this.Drawable;
                if (drawable == null || drawable.IntrinsicWidth == 0 || drawable.IntrinsicHeight == 0)
                    return;
                var bmWidth = drawable.IntrinsicWidth;
                var bmHeight = drawable.IntrinsicHeight;

                //Log.d("bmSize", "bmWidth: " + bmWidth + " bmHeight : " + bmHeight);

                var scaleX = (float)_viewWidth / (float)bmWidth;
                var scaleY = (float)_viewHeight / (float)bmHeight;
                scale = Math.Min(scaleX, scaleY);
                _matrix.SetScale(scale, scale);

                // Center the image
                var redundantYSpace = (float)_viewHeight - (scale * (float)bmHeight);
                var redundantXSpace = (float)_viewWidth - (scale * (float)bmWidth);
                redundantYSpace /= (float)2;
                redundantXSpace /= (float)2;

                _matrix.PostTranslate(redundantXSpace, redundantYSpace);

                OrigWidth = _viewWidth - 2 * redundantXSpace;
                OrigHeight = _viewHeight - 2 * redundantYSpace;
                this.ImageMatrix = _matrix;
            }
            FixTrans();
        }



        public bool OnScale(ScaleGestureDetector detector)
        {
            var mScaleFactor = detector.ScaleFactor;

            var origScale = _saveScale;

            _saveScale *= mScaleFactor;

            if (_saveScale > _maxScale)
            {

                _saveScale = _maxScale;

                mScaleFactor = _maxScale / origScale;

            }
            else if (_saveScale < MIN_SCALE)
            {

                _saveScale = MIN_SCALE;

                mScaleFactor = MIN_SCALE / origScale;

            }



            if (OrigWidth * _saveScale <= _viewWidth || OrigHeight * _saveScale <= _viewHeight)

                _matrix.PostScale(mScaleFactor, mScaleFactor, _viewWidth / 2, _viewHeight / 2);

            else

                _matrix.PostScale(mScaleFactor, mScaleFactor, detector.FocusX, detector.FocusY);



            FixTrans();

            return true;
        }

        public bool OnScaleBegin(ScaleGestureDetector detector)
        {
            _mode = ZOOM;
            return true;
        }

        public void OnScaleEnd(ScaleGestureDetector detector)
        {
        }
    }
}