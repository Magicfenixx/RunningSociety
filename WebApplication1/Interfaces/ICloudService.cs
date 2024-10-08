﻿using CloudinaryDotNet.Actions;

namespace WebApplication1.Interfaces
{
    public interface ICloudService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
