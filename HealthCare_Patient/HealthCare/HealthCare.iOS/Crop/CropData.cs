using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UIKit;

namespace HealthCare.iOS.Crop
{
    public class CropData
    {
        public UIColor BorderColor
        {
            get;
            set;
        }

        public float BorderWidth
        {
            get;
            set;
        }

        public float CircleRadius
        {
            get;
            set;
        }

        public CropData.CroppingTypes CropType
        {
            get;
            set;
        }

        public float ElipseHeight
        {
            get;
            set;
        }

        public float ElipseWidth
        {
            get;
            set;
        }

        public UIColor FillColor
        {
            get;
            set;
        }

        public float MaximumZoomScale
        {
            get;
            set;
        }

        public float MinimumZoomScale
        {
            get;
            set;
        }

        public float SquareHeight
        {
            get;
            set;
        }

        public float SquareWidth
        {
            get;
            set;
        }

        public float StartZoomScale
        {
            get;
            set;
        }

        public CropData()
        {
            this.CropType = CropData.CroppingTypes.Circle;
            this.StartZoomScale = 0.25f;
            this.MaximumZoomScale = 3f;
            this.MinimumZoomScale = 0.122f;
            this.SquareHeight = 200f;
            this.SquareWidth = 200f;
            this.CircleRadius = 148f;
            this.ElipseHeight = 150f;
            this.ElipseWidth = 200f;
            this.BorderWidth = 1f;
            this.BorderColor = UIColor.FromRGBA(4, 4, 4, 150);
            this.FillColor = UIColor.FromRGBA(4, 4, 4, 150);
        }

        public RectangleF GetCircleRectangleF()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            var width = bounds.Width / 2f - this.CircleRadius;
            var rectangleF = UIScreen.MainScreen.Bounds;
            return new RectangleF((float)width, (float)rectangleF.Height / 2f - this.CircleRadius, this.CircleRadius * 2f, this.CircleRadius * 2f);
        }

        public RectangleF GetCroppingRectangleF()
        {
            if (this.CropType == CropData.CroppingTypes.Circle)
            {
                return this.GetCircleRectangleF();
            }
            if (this.CropType == CropData.CroppingTypes.Elipse)
            {
                return this.GetElipseRectangleF();
            }
            if (this.CropType == CropData.CroppingTypes.Square)
            {
                return this.GetSquareRectangleF();
            }
            var width = UIScreen.MainScreen.Bounds.Width;
            var bounds =  UIScreen.MainScreen.Bounds;
            return new RectangleF(0f, 0f, (float)width, (float)bounds.Height);
        }

        public RectangleF GetElipseRectangleF()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            var width = bounds.Width/ 2f - this.ElipseWidth / 2f;
            var rectangleF = UIScreen.MainScreen.Bounds;
            return new RectangleF((float)width, (float)rectangleF.Height / 2f - this.ElipseHeight / 2f, this.ElipseWidth, this.ElipseHeight);
        }

        public RectangleF GetSquareRectangleF()
        {
            var bounds = UIScreen.MainScreen.Bounds;
            var width = bounds.Width / 2f - this.SquareWidth / 2f;
            var rectangleF = UIScreen.MainScreen.Bounds;
            return new RectangleF((float)width, (float)rectangleF.Height / 2f - this.SquareHeight / 2f, this.SquareWidth, this.SquareHeight);
        }

        public enum CroppingTypes
        {
            Full,
            Square,
            Circle,
            Elipse
        }
    }
}
