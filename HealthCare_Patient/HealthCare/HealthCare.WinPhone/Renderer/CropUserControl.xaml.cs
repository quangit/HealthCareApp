using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Storage;
using ExifLib;
using HealthCare.ViewModels;

namespace HealthCare.WinPhone.Renderer
{
    public partial class CropUserControl : UserControl
    {
        private double _clipLeftPerc, _clipRightPerc, _clipTopPerc, _clipBotPerc;
        private Rectangle _draggedRect;
        private JpegInfo exif;
        private StorageFile file;

        private bool isMoving;
        private bool isStartDrawing;
        private double saveX;
        private double saveY;
        private WriteableBitmap writableBitmap;

        public CropUserControl()
        {
            InitializeComponent();

            Init();

            PhotoEditViewModel.Instance.CropEvent += SaveImage;
        }

        private bool isClickCrop;

        public CropUserControl(byte[] byteArray)
        {
            InitializeComponent();
            Init();
            isClickCrop = true;
            PhotoEditViewModel.Instance.CropEvent += SaveImage;
            SetImageSource(byteArray);
        }

        public void SetImageSource(byte[] byteArray)
        {
            using (var streamIn = new MemoryStream(byteArray))
            {
                exif = ExifReader.ReadJpeg(streamIn);
            }

            using (var streamIn = new MemoryStream(byteArray))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(streamIn);
                writableBitmap = new WriteableBitmap(bitmapImage);
            }

            imgSauce.Source = writableBitmap;
            ImgBackground.Source = writableBitmap;
        }

        private void Init()
        {
            var rects = new[] {rectTopRight, rectTopLeft, rectBotRight, rectBotLeft};
            var _dragOrigin = new Point();
            double origLeftPerc = 0, origRightPerc = 0, origTopPerc = 0, origBotPerc = 0;

            var setOrigin = new Action<Point>(p =>
            {
                _dragOrigin = p;
                origLeftPerc = _clipLeftPerc;
                origRightPerc = _clipRightPerc;
                origTopPerc = _clipTopPerc;
                origBotPerc = _clipBotPerc;
            });

            foreach (var aRect in rects)
            {
                aRect.MouseLeftButtonDown += (s, e) =>
                {
                    var r = (Rectangle) s;
                    _draggedRect = r;
                    setOrigin(e.GetPosition(imgSauce));

                    r.CaptureMouse();
                };

                aRect.MouseLeftButtonUp += (s, e) => { _draggedRect = null; };

                aRect.MouseMove += (s, e) =>
                {
                    if (_draggedRect != null)
                    {
                        var pos = e.GetPosition(imgSauce);

                        if (s == rectTopLeft || s == rectTopRight)
                        {
                            // Adjust top
                            _clipTopPerc = origTopPerc + (pos.Y - _dragOrigin.Y)/imgSauce.ActualHeight;
                        }
                        if (s == rectTopLeft || s == rectBotLeft)
                        {
                            // Adjust Left
                            _clipLeftPerc = origLeftPerc + (pos.X - _dragOrigin.X)/imgSauce.ActualWidth;
                        }
                        if (s == rectBotLeft || s == rectBotRight)
                        {
                            // Adjust bottom
                            _clipBotPerc = origBotPerc - (pos.Y - _dragOrigin.Y)/imgSauce.ActualHeight;
                        }
                        if (s == rectTopRight || s == rectBotRight)
                        {
                            // Adjust Right
                            _clipRightPerc = origRightPerc - (pos.X - _dragOrigin.X)/imgSauce.ActualWidth;
                        }

                        isStartDrawing = true;

                        updateClipAndTransforms();
                    }
                };
            }

            var draggingImg = false;

            imgSauce.MouseLeftButtonDown += (s, e) =>
            {
                setOrigin(e.GetPosition(imgSauce));
                imgSauce.CaptureMouse();
                draggingImg = true;

                isMoving = true;
            };

            imgSauce.MouseLeftButtonUp += (s, e) =>
            {
                isMoving = false;
                draggingImg = false;
            };

            imgSauce.MouseMove += (s, e) =>
            {
                if (draggingImg)
                {
                    var pos = e.GetPosition(imgSauce);

                    var xAdjust = (pos.X - _dragOrigin.X)/imgSauce.ActualWidth;
                    var yAdjust = (pos.Y - _dragOrigin.Y)/imgSauce.ActualHeight;

                    _clipLeftPerc = origLeftPerc + xAdjust;
                    _clipRightPerc = origRightPerc - xAdjust;
                    _clipTopPerc = origTopPerc + yAdjust;
                    _clipBotPerc = origBotPerc - yAdjust;

                    isStartDrawing = true;

                    updateClipAndTransforms();
                }
            };

            _clipLeftPerc = 0.2;
            _clipRightPerc = 0.2;
            _clipTopPerc = 0.2;
            _clipBotPerc = 0.2;


            imgSauce.SizeChanged += (x, y) => { updateClipAndTransforms(); };

            updateClipAndTransforms();
        }

        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    if (e.NavigationMode == NavigationMode.New)
        //    {
        //        GC.Collect();
        //        IDictionary<string, string> para = NavigationContext.QueryString;

        //        string nameIma = "";

        //        if (para.ContainsKey("name"))
        //            nameIma = para["name"];

        //        int rotation = 0;
        //        if (para.ContainsKey("rotato"))
        //            rotation = Convert.ToInt32(para["rotato"]);

        //        //ShareContext.Context.rotation = rotation;

        //        var folder = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(CapturePhoto.FOLDER_PHOTOS, CreationCollisionOption.OpenIfExists);
        //        file = await folder.GetFileAsync(nameIma);
        //        BitmapImage img = new BitmapImage();

        //        int rotated = 0;

        //        if (ShareContext.Context.CroppedImage == null)
        //        {
        //            using (var stream = await file.OpenAsync(FileAccessMode.Read))
        //            {
        //                img.SetSource(stream.AsStream());
        //            }
        //            var p = await file.Properties.GetImagePropertiesAsync();

        //            if (p.Orientation == PhotoOrientation.Rotate90)
        //            {
        //                rotated = 90;
        //            }
        //            else if (p.Orientation == PhotoOrientation.Rotate180)
        //            {
        //                rotated = 180;
        //            }
        //            else if (p.Orientation == PhotoOrientation.Rotate270)
        //            {
        //                rotated = 270;
        //            }
        //        }

        //        if (ShareContext.Context.CroppedImage == null)
        //            writableBitmap = new WriteableBitmap(img);
        //        else
        //            writableBitmap = ShareContext.Context.CroppedImage;

        //        rotation = ShareContext.Context.rotation - rotated;

        //        writableBitmap = writableBitmap.Rotate(rotation);

        //        imgSauce.Source = writableBitmap;
        //        ImgBackground.Source = writableBitmap;

        //        img.UriSource = null;
        //        img = null;
        //    }
        //}

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    base.OnNavigatedFrom(e);

        //    if (e.NavigationMode == NavigationMode.Back)
        //    {
        //        imgSauce.Source = null;
        //        ImgBackground.Source = null;
        //        writableBitmap = null;
        //    }
        //}

        private void updateClipAndTransforms()
        {
            // Check bounds
            if (_clipLeftPerc + _clipRightPerc >= 1)
                _clipLeftPerc = 1 - _clipRightPerc - 0.04;
            if (_clipTopPerc + _clipBotPerc >= 1)
                _clipTopPerc = 1 - _clipBotPerc - 0.04;

            if (_clipLeftPerc < 0)
                _clipLeftPerc = 0;
            if (_clipRightPerc < 0)
                _clipRightPerc = 0;
            if (_clipBotPerc < 0)
                _clipBotPerc = 0;
            if (_clipTopPerc < 0)
                _clipTopPerc = 0;
            if (_clipLeftPerc >= 1)
                _clipLeftPerc = 0.99;
            if (_clipRightPerc >= 1)
                _clipRightPerc = 0.99;
            if (_clipBotPerc >= 1)
                _clipBotPerc = 0.99;
            if (_clipTopPerc >= 1)
                _clipTopPerc = 0.99;

            var leftX = _clipLeftPerc*imgSauce.ActualWidth;
            var topY = _clipTopPerc*imgSauce.ActualHeight;

            clipRect.Rect = new Rect(leftX, topY, (1 - _clipRightPerc)*imgSauce.ActualWidth - leftX,
                (1 - _clipBotPerc)*imgSauce.ActualHeight - topY);

            if (clipRect.Rect.Width == 0 && clipRect.Rect.Height == 0)
            {
                rectTopLeft.Visibility = rectTopRight.Visibility =
                    rectBotLeft.Visibility = rectBotRight.Visibility = Visibility.Collapsed;
            }
            else
            {
                rectTopLeft.Visibility = rectTopRight.Visibility =
                    rectBotLeft.Visibility = rectBotRight.Visibility = Visibility.Visible;
            }

            // Rectangle Transforms
            ((TranslateTransform) rectTopLeft.RenderTransform).X = clipRect.Rect.X;
            ((TranslateTransform) rectTopLeft.RenderTransform).Y = clipRect.Rect.Y;

            ((TranslateTransform) rectTopRight.RenderTransform).X = -_clipRightPerc*imgSauce.ActualWidth;
            ((TranslateTransform) rectTopRight.RenderTransform).Y = clipRect.Rect.Y;

            ((TranslateTransform) rectBotLeft.RenderTransform).X = clipRect.Rect.X;
            ((TranslateTransform) rectBotLeft.RenderTransform).Y = -_clipBotPerc*imgSauce.ActualHeight;

            ((TranslateTransform) rectBotRight.RenderTransform).X = -_clipRightPerc*imgSauce.ActualWidth;
            ((TranslateTransform) rectBotRight.RenderTransform).Y = -_clipBotPerc*imgSauce.ActualHeight;
        }

        private void btnRigth_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                writableBitmap = writableBitmap.Rotate(90);

                imgSauce.Source = writableBitmap;
                ImgBackground.Source = writableBitmap;
            });
        }

        private void txtCancel_Tap(object sender, GestureEventArgs e)
        {
            //ShareContext.Context.CroppedImage = null;
            //PhotoEdit.IsCropCancellation = true;
            //NavigationService.GoBack();
        }

        private void txtSave_Tap(object sender, GestureEventArgs e)
        {
            //SaveImage();
        }

        private void SaveImage(object sender, EventArgs args)
        {
            try
            {
                // Get the size of the source image
                double originalImageWidth = writableBitmap.PixelWidth;
                double originalImageHeight = writableBitmap.PixelHeight;

                // Get the size of the image when it is displayed on the phone
                var displayedWidth = ImgBackground.ActualWidth;
                var displayedHeight = ImgBackground.ActualHeight;

                // Calculate the ratio of the original image to the displayed image
                var widthRatio = originalImageWidth/displayedWidth;
                var heightRatio = originalImageHeight/displayedHeight;

                // Create a new WriteableBitmap. The size of the bitmap is the size of the cropping rectangle
                // drawn by the user, multiplied by the image size ratio.
                var WB_CroppedImage = new WriteableBitmap((int) (widthRatio*Math.Abs(clipRect.Rect.Width)),
                    (int) (heightRatio*Math.Abs(clipRect.Rect.Height)));

                // Calculate the offset of the cropped image. This is the distance, in pixels, to the top left corner
                // of the cropping rectangle, multiplied by the image size ratio.
                var xoffset = (int) (clipRect.Rect.X*widthRatio);
                var yoffset = (int) (clipRect.Rect.Y*heightRatio);

                // Copy the pixels from the targeted region of the source image into the target image, 
                // using the calculated offset
                if (WB_CroppedImage.Pixels.Length > 0)
                {
                    for (var i = 0; i < WB_CroppedImage.Pixels.Length; i++)
                    {
                        var x = i%WB_CroppedImage.PixelWidth + xoffset;
                        var y = i/WB_CroppedImage.PixelWidth + yoffset;
                        WB_CroppedImage.Pixels[i] = writableBitmap.Pixels[y*writableBitmap.PixelWidth + x];
                    }

                    // Set the source of the image control to the new cropped bitmap
                    //FinalCroppedImage.Source = WB_CroppedImage;
                    //SaveBtn.Visibility = Visibility.Visible;

                    //ShareContext.Context.CroppedImage = WB_CroppedImage;
                    //ShareContext.Context.rotation = 0;
                }

                var byteArray = new byte[0];
                using (var stream = new MemoryStream())
                {
                    WB_CroppedImage.SaveJpeg(stream, WB_CroppedImage.PixelWidth, WB_CroppedImage.PixelHeight, 0, 100);
                    stream.Close();

                    byteArray = stream.ToArray();
                }
                if (isClickCrop)
                {
                    isClickCrop = false;
                    UserViewModel.Instance.UpdateAvatar(byteArray);
                }
                
                //PhotoEditViewModel.Instance.GoBack();
            }
            catch (Exception ex)
            {
                //ShareContext.Context.CroppedImage = null;
            }
            //finally { NavigationService.GoBack(); }
        }
    }
}