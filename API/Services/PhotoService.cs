using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API;

public class PhotoService : IPhotoService
{

    private readonly Cloudinary _cloudinary;
    public PhotoService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(acc);
    }

    /**
     * This method is used to add a photo to the cloudinary
     * @param file: IFormFile
     * @return Task<ImageUploadResult>
     */
    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file){
        var uploadResult = new ImageUploadResult();
        if(file.Length > 0){
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams{
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500)
                .Crop("fill").Gravity("face")
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
            return uploadResult;
    }

    /**
     * This method is used to delete a photo from the cloudinary
     * @param publicId: string
     * @return Task<DeletionResult>
     */
    public async Task<DeletionResult> DeletePhotoAsync(string publicId){
        var deleteParams = new DeletionParams(publicId);
        return await  _cloudinary.DestroyAsync(deleteParams);
    }
}