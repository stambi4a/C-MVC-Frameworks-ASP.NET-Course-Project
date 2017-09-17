namespace Helpers
{
    using System.Web.Helpers;

    public class ImageHelper
    {
        private const int PlayerImageSize = 150;

        public static void CropPlayerImage(WebImage photo)
        {
            CropImage(photo, PlayerImageSize);
        }

        private static void CropImage(WebImage photo, int size)
        {
            var top = 0;
            var left = 0;
            var bottom = 0;
            if (photo.Width > photo.Height)
            {
                photo.Resize(photo.Width, size, true);
                left = (photo.Width - size) / 2;
            }
            else
            {
                photo.Resize(size, photo.Height, true);
                left = 0;
                bottom = photo.Height - size;
            }

            photo.Crop(top, left, bottom, left);
        }
    }
}
