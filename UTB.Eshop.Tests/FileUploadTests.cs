using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Xunit;
using Xunit.Abstractions;
using Moq;

using UTB.Eshop.Domain.Implementation;

using UTB.Eshop.Tests.Helpers;

namespace UTB.Eshop.Tests
{
    public class FileUploadTests
    {
        const string relativeDirectoryPath = "/images";


        private readonly ITestOutputHelper _testOutputHelper;
        public FileUploadTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void CreateOutputDirectoryForUploadTest_Success()
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + relativeDirectoryPath));

            Assert.NotNull(directoryInfo);

            _testOutputHelper.WriteLine(directoryInfo.FullName);
            //Directory.Delete(directoryInfo.FullName, true);
        }


        [Fact]
        public async Task UploadImageFile_Success()
        {

            //Arrange
            string content = "?PNG" + "FakeImageContent";
            string fileName = "UploadImageFile.png";

            string ImageSrc = String.Empty;

            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + relativeDirectoryPath));


            string relativePath = Path.Combine(relativeDirectoryPath, fileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            //nastaveni fakeove IFormFile pomoci MemoryStream
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    MockHelperFormFile iffMockHelper = new MockHelperFormFile(_testOutputHelper);
                    Mock<IFormFile> iffMock = iffMockHelper.MockIFormFile(ms, writer, fileName, content, "image/png");


                    FileUpload fup = new FileUpload(Directory.GetCurrentDirectory());
                    fup.ContentType = "image";
                    fup.FileLength = 5_000_000;

                    //Act
                    ImageSrc = await fup.FileUploadAsync(iffMock.Object, "images");
                }
            }

            string pathFromImageSource = Directory.GetCurrentDirectory() + ImageSrc;



            //Assert
            Assert.False(String.IsNullOrWhiteSpace(ImageSrc));

            _testOutputHelper.WriteLine("vytvorena cesta path: " + path);
            _testOutputHelper.WriteLine("vytvorena cesta relativePath: " + relativePath);
            _testOutputHelper.WriteLine("vytvorena cesta ImageSrc: " + ImageSrc);
            _testOutputHelper.WriteLine("vytvorena cesta CurrentDirectory+ImageSrc: " + pathFromImageSource);
            Assert.True(ImageSrc == relativePath);

            Assert.True(File.Exists(pathFromImageSource));

            if (File.Exists(pathFromImageSource))
            {
                File.Delete(pathFromImageSource);
                //Assert.False(File.Exists(pathFromImageSource));
            }
        }


        [Fact]
        public async Task UploadTextFile_Failure()
        {

            //Arrange
            string content = "Fake text content";
            string fileName = "UploadTextFile.txt";

            string ImageSrc = String.Empty;

            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory() + relativeDirectoryPath));

            string relativePath = Path.Combine(relativeDirectoryPath, fileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            //nastaveni fakeove IFormFile pomoci MemoryStream
            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    MockHelperFormFile iffMockHelper = new MockHelperFormFile(_testOutputHelper);
                    Mock<IFormFile> iffMock = iffMockHelper.MockIFormFile(ms, writer, fileName, content, "text/txt");


                    FileUpload fup = new FileUpload(Directory.GetCurrentDirectory());
                    fup.ContentType = "image";
                    fup.FileLength = 5_000_000;

                    //Act
                    ImageSrc = await fup.FileUploadAsync(iffMock.Object, "Carousels");
                }
            }
            string pathFromImageSource = Directory.GetCurrentDirectory() + ImageSrc;



            //Assert
            Assert.True(String.IsNullOrWhiteSpace(ImageSrc));

            _testOutputHelper.WriteLine("vytvorena cesta path: " + path);
            _testOutputHelper.WriteLine("vytvorena cesta relativePath: " + relativePath);
            _testOutputHelper.WriteLine("vytvorena cesta ImageSrc: " + ImageSrc);
            _testOutputHelper.WriteLine("vytvorena cesta CurrentDirectory+ImageSrc: " + pathFromImageSource);
            Assert.False(ImageSrc == relativePath);

            Assert.False(File.Exists(pathFromImageSource));

            if (File.Exists(pathFromImageSource))
                File.Delete(pathFromImageSource);
        }

    }
}
