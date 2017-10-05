using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Toolbox.Portable.Utilities
{
	public static class ImageUtility<T>
	{
        private static HttpClient downloadClient = new HttpClient();

        public static async Task<T> RetrieveImageAsync(string imageUrl, Func<byte[], T> prepareImage)
		{
			if (string.IsNullOrWhiteSpace(imageUrl))
			{
				throw new ArgumentException("Parameter imageUrl cannot be null or whitepace");
			}

            if (prepareImage == null)
            {
                throw new ArgumentException("Parameter prepareImage cannot be null");
            }

			byte[] imageBytes = await downloadClient.GetByteArrayAsync(imageUrl);

			if (imageBytes == null)
			{
				throw new Exception("Unable to download image from requested Url");
			}

            T image = prepareImage(imageBytes);
			imageBytes = null;

			return image;
		}
	}
}