using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        static readonly string Directory = System.IO.Directory.GetCurrentDirectory() + @"\wwwroot\uploads\";
        static string path = @"images\";

        public static async Task<string> Add(IFormFile file)
        {
            return await Task.Run(() =>
            {
                string extension = Path.GetExtension(file.FileName)?.ToUpper();
                string newFileName = Guid.NewGuid().ToString("N") + extension;


                if (!System.IO.Directory.Exists(Directory + path))
                {
                    System.IO.Directory.CreateDirectory(Directory + path);
                }

                //Check if its valid image.
                using MemoryStream ms = new MemoryStream();
                file.CopyToAsync(ms);

                byte[] bytes = ms.ToArray();

                if (!bytes.IsImage().Result)
                {
                    throw new ArgumentException($" {bytes.Length}  - It is not valid image!");
                }

                //Upload image
                using FileStream fileStream = File.Create(Directory + path + newFileName);
                file.CopyTo(fileStream);
                fileStream.Flush();

                return (path + newFileName).Replace("\\", "/");
            });
        }

        public static async Task<string> Update(IFormFile file, string oldImagePath)
        {
            return await Task.Run(() =>
            {
                Delete(oldImagePath);
                return Add(file);
            });
        }

        public static async void Delete(string imagePath)
        {
            await Task.Run(() =>
            {
                if (File.Exists(Directory + imagePath.Replace("/", "\\")) &&
                    Path.GetFileName(imagePath) != "default.png")
                {
                    File.Delete(Directory + imagePath.Replace("/", "\\"));
                }
            });
        }

        private static async Task<bool> IsImage(this byte[] fileBytes)
        {
            return await Task.Run(() =>
            {
                var headers = new List<byte[]>
                {
                    Encoding.ASCII.GetBytes("BM"), // BMP
                    Encoding.ASCII.GetBytes("GIF"), // GIF
                    new byte[] {137, 80, 78, 71}, // PNG
                    new byte[] {73, 73, 42}, // TIFF
                    new byte[] {77, 77, 42}, // TIFF
                    new byte[] {255, 216, 255, 224}, // JPEG
                    new byte[] {255, 216, 255, 225} // JPEG CANON
                };

                return headers.Any(x => x.SequenceEqual(fileBytes.Take(x.Length)));
            });
        }
    }
}