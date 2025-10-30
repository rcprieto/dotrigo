using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace API.Domain.Auxiliares
{
    public static class ResizeImage
    {
        private const int OrientationKey = 0x0112;
        private const int NotSpecified = 0;
        private const int NormalOrientation = 1;
        private const int MirrorHorizontal = 2;
        private const int UpsideDown = 3;
        private const int MirrorVertical = 4;
        private const int MirrorHorizontalAndRotateRight = 5;
        private const int RotateLeft = 6;
        private const int MirorHorizontalAndRotateLeft = 7;
        private const int RotateRight = 8;
        public static Bitmap Resize(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);



            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Image Resize(Image imgPhoto, int newValue)
        {

            int newWidth = 0;
            int newHeight = 0;
            if (imgPhoto.Width > 500 || imgPhoto.Height > 500)
            {
                if (imgPhoto.Width >= imgPhoto.Height)
                {
                    float nPercent = ((float)newValue / (float)imgPhoto.Width);
                    newWidth = System.Convert.ToInt16(imgPhoto.Width * nPercent);
                    newHeight = System.Convert.ToInt16(imgPhoto.Height * nPercent);
                }
                else
                {
                    float nPercent = ((float)newValue / (float)imgPhoto.Height);
                    newWidth = System.Convert.ToInt16(imgPhoto.Width * nPercent);
                    newHeight = System.Convert.ToInt16(imgPhoto.Height * nPercent);
                }
            }
            else
            {
                newWidth = imgPhoto.Width;
                newHeight = imgPhoto.Height;
            }

            if (imgPhoto.PropertyIdList.Contains(OrientationKey))
            {
                var orientation = (int)imgPhoto.GetPropertyItem(OrientationKey).Value[0];
                switch (orientation)
                {
                    case NotSpecified: // Assume it is good.
                    case NormalOrientation:
                        // No rotation required.
                        break;
                    case MirrorHorizontal:
                        imgPhoto.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case UpsideDown:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case MirrorVertical:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case MirrorHorizontalAndRotateRight:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case RotateLeft:
                        {
                            imgPhoto.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            int temp = newWidth;
                            newWidth = newHeight;
                            newHeight = temp;
                        }
                        break;
                    case MirorHorizontalAndRotateLeft:
                        imgPhoto.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case RotateRight:
                        {
                            imgPhoto.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            int temp = newWidth;
                            newWidth = newHeight;
                            newHeight = temp;
                        }
                        break;
                    default:
                        throw new NotImplementedException("An orientation of " + orientation + " isn't implemented.");
                }
            }

            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);
            destImage.SetResolution(72, 72);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var imageRectangle = new Rectangle(0, 0, imgPhoto.Width, imgPhoto.Height);
                //wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(imgPhoto, destRect);

            }

            return destImage;
        }


     



    }
}