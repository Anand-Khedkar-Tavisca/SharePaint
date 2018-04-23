using Moq;
using SharePaint.Data.Models;
using SharePaint.Services.ServiceProviders;
using ShareStroke.Services.ServiceProviders;
using System;
using Xunit;

namespace SharePaint.Tests
{
    public class ReceiveDataHandlerTest
    {
        [Fact]
        public void NoDataShouldReturnFalse()
        {
            //arrenge
            Mock<IStrokeService> strokeServiceMock = new Mock<IStrokeService>();
            strokeServiceMock.Setup(e => e.Update(It.IsAny<Stroke>())).Returns((Stroke Stroke) => {
                Stroke.Id = Guid.NewGuid();
                return Stroke;
            });

            Mock<ILineService> lineServiceMock = new Mock<ILineService>();
            lineServiceMock.Setup(e => e.CreateLine(It.IsAny<Line>())).Returns((Line line) => {
                line.Id = Guid.NewGuid();
                return line;
            });
            var reviceDataHandler = new ReceiveDataHandler(lineServiceMock.Object, strokeServiceMock.Object);

            //act
            var sucess = reviceDataHandler.Handle(null);

            //assert
            Assert.False(sucess.IsSucess);
        }
        [Fact]
        public void InvalidJsonShouldReturnFalse()
        {
            //arrenge
            Mock<ILineService> lineServiceMock = new Mock<ILineService>();
            lineServiceMock.Setup(e => e.CreateLine(It.IsAny<Line>())).Returns((Line line) => {
                line.Id = Guid.NewGuid();
                return line;
            });
            Mock<IStrokeService> strokeServiceMock = new Mock<IStrokeService>();
            strokeServiceMock.Setup(e => e.Update(It.IsAny<Stroke>())).Returns((Stroke Stroke) => {
                Stroke.Id = Guid.NewGuid();
                return Stroke;
            });

            var reviceDataHandler = new ReceiveDataHandler(lineServiceMock.Object, strokeServiceMock.Object);


            //act
            var sucess = reviceDataHandler.Handle("invalid json");

            //assert
            Assert.False(sucess.IsSucess);
        }

        [Fact]
        public void ValidJsonShouldInvokeCreateLineTest()
        {
            //arrenge
            Mock<ILineService> lineServiceMock = new Mock<ILineService>();
            lineServiceMock.Setup(e => e.CreateLine(It.IsAny<Line>())).Returns((Line line) => {
                line.Id = Guid.NewGuid();
                return line;
            });
            Mock<IStrokeService> strokeServiceMock = new Mock<IStrokeService>();
            strokeServiceMock.Setup(e => e.Update(It.IsAny<Stroke>())).Returns((Stroke Stroke) => {
                Stroke.Id = Guid.NewGuid();
                return Stroke;
            });
            var reviceDataHandler = new ReceiveDataHandler(lineServiceMock.Object, strokeServiceMock.Object);


            //act
            var sucess = reviceDataHandler.Handle("{\"StartPoint\":{\"X\":395,\"Y\":194},\"EndPoint\":{\"X\":392,\"Y\":194},\"StrokeId\":\"1dc80907-959a-49cf-b2cd-03f6518ce2fa\"}");

            //assert
            Assert.True(sucess.IsSucess);
            lineServiceMock.Verify(e => e.CreateLine(It.IsAny<Line>()), Times.Once);

        }

        [Fact]
        public void ValidJsonWithStartPointNull_ShouldReturnFalseAndNotInvokeCreateLineTest()
        {
            //arrenge
            Mock<ILineService> lineServiceMock = new Mock<ILineService>();
            lineServiceMock.Setup(e => e.CreateLine(It.IsAny<Line>())).Returns((Line line) => {
                line.Id = Guid.NewGuid();
                return line;
            });

            Mock<IStrokeService> strokeServiceMock = new Mock<IStrokeService>();
            strokeServiceMock.Setup(e => e.Update(It.IsAny<Stroke>())).Returns((Stroke Stroke) => {
                Stroke.Id = Guid.NewGuid();
                return Stroke;
            });
            var reviceDataHandler = new ReceiveDataHandler(lineServiceMock.Object, strokeServiceMock.Object);

            //act
            var sucess = reviceDataHandler.Handle("{\"EndPoint\":{\"X\":392,\"Y\":194},\"StrokeId\":\"1dc80907-959a-49cf-b2cd-03f6518ce2fa\"}");

            //assert
            Assert.False(sucess.IsSucess);
            lineServiceMock.Verify(e => e.CreateLine(It.IsAny<Line>()), Times.Never);
        }


        //[Fact]
        //public void InvalidJsonShouldReturnFalseANdLogException()
        //{
        //    //arrenge
        //    Mock<ILineService> lineServiceMock = new Mock<ILineService>();
        //    lineServiceMock.Setup(e => e.CreateLine(It.IsAny<Line>())).Returns((Line line) =>
        //    {
        //        line.Id = Guid.NewGuid();
        //        return line;
        //    });
        //    Mock<ReceiveDataHandler> reviceDataHandlerMock = new Mock<ReceiveDataHandler>(lineServiceMock.Object) { CallBase = true };

        //    //act
        //    var sucess = reviceDataHandlerMock.Object.Handle("invalid json");

        //    //assert
        //    Assert.False(sucess.IsSucess);
        //    reviceDataHandlerMock.Verify(e => e.LogException(), Times.Once);
        //}

    }
}
