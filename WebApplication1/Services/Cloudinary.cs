﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WebApplication1.Helpers;
using WebApplication1.Interfaces;

namespace WebApplication1.Services
{
    public class Cloudinary : ICloudinary
    {
        private readonly Cloudinary _cloudinary;
        public Cloudinary(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
