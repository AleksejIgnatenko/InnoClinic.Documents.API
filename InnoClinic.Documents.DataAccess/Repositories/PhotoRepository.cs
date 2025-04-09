using InnoClinic.Documents.DataAccess.Core;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;

namespace InnoClinic.Documents.DataAccess.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMinioClient _minioClient;
        private const string BUCKET_NAME = "photos";

        public PhotoRepository(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task CreateOrUpdateAsync(IFormFile photo, string id)
        {
            await IsBucketExist();

            using (var stream = photo.OpenReadStream())
            {
                await _minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(BUCKET_NAME)
                    .WithObject(id)
                    .WithStreamData(stream)
                    .WithObjectSize(photo.Length));
            }
        }

        public async Task<Stream> GetByIdAsync(string id)
        {
            await IsBucketExist();

            var memoryStream = new MemoryStream();

            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(BUCKET_NAME)
                .WithObject(id)
                .WithCallbackStream((stream) =>
                {
                    stream.CopyTo(memoryStream);
                }));

            memoryStream.Position = 0;

            return memoryStream;
        }

        private async Task IsBucketExist()
        {
            bool isExist = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(BUCKET_NAME));
            if (!isExist)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(BUCKET_NAME));
            }
        }
    }
}
