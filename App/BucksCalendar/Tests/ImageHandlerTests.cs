using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using BucksCalendar.Utilities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Tests
{
    public class ImageHandlerTests
    {
        [Theory]
        [InlineData(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x7 }, "AQIDBAUGBw==")]
        [InlineData(new byte[] { 0xe1, 0x0e, 0x06, 0x10, 0xb2, 0x50, 0x3d }, "4Q4GELJQPQ==")]
        [InlineData(new byte[] { 0x01, 0x04, 0xc6, 0xff, 0x0d, 0x70, 0x9d }, "AQTG/w1wnQ==")]
        public void EncodeImageSuccessfully(byte[] input, string result)
        {
            Assert.Equal($"data:image/jpg;base64,{result}", ImageHandler.ToBase64ImgString(input));
        }
        
        [Fact]
        public async void SuccessfulImageToMemoryStream()
        {
            var fileContents = new MemoryStream(Encoding.UTF8.GetBytes("Test file"));
            IFormFile testFile = new FormFile(fileContents, 0, 9, "Data", "test.txt");
            
            Assert.Equal(fileContents.ToArray(), await ImageHandler.ToMemoryStreamAsync(testFile));
        }
        
        [Fact]
        public void ImageToMemoryStreamException()
        {
            var fileContents = new MemoryStream(new byte[2097155]);
            IFormFile testFile = new FormFile(fileContents, 0, 9, "Data", "test.txt");
            
            Assert.ThrowsAsync<ImageFormatLimitationException>(async () => await ImageHandler.ToMemoryStreamAsync(testFile));
        }
    }
}