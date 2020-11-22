using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Goder.BL.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> GetImageFromUrlAsBase64String(string imageUrl)
        {
            WebClient client = new WebClient();
            Stream stream = null;
            try
            {
                stream = await client.OpenReadTaskAsync(imageUrl);
            }
            catch
            {
                return await GenerateImageWithRandomColor();
            }
            byte[] bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);

                if (!await isValidImage(ms))
                {
                    return await GenerateImageWithRandomColor();
                }

                bytes = ms.ToArray();
                await ms.FlushAsync();
            }

            await stream.FlushAsync();
            stream.Dispose();
            client.Dispose();

            return Convert.ToBase64String(bytes);
        }

        public static async Task<string> ConvertImageToBase64String(IFormFile file)
        {
            byte[] bytes;

            if (file == null || file.Length == 0)
            {
                throw new Exception("Empty file");
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                bytes = ms.ToArray();
                await ms.FlushAsync();
            }

            return Convert.ToBase64String(bytes);
        }

        public static async Task<string> GenerateImageWithRandomColor()
        {
            Random r = new Random();
            Bitmap img = new Bitmap(512, 512);
            Graphics drawing = Graphics.FromImage(img);
            byte[] bytes;

            drawing.Clear(Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256)));

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);
                bytes = ms.ToArray();
                await ms.FlushAsync();
            }

            drawing.Dispose();
            img.Dispose();

            return Convert.ToBase64String(bytes);
        }

        private static Task<bool> isValidImage(MemoryStream ms)
        {
            return Task.Run(() =>
            {
                try
                {
                    Image.FromStream(ms);
                }
                catch (ArgumentException)
                {
                    return false;
                }
                return true;
            });
        }
    }
}
