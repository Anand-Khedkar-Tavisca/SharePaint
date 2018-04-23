using Moq;
using ShareLine.Data.Services;
using ShareLine.Services.ServiceProviders;
using SharePaint.Data.Models;
using SharePaint.Data.Services;
using SharePaint.Services.ServiceProviders;
using ShareStroke.Data.Services;
using ShareStroke.Services.ServiceProviders;
using ShareUser.Data.Services;
using System;
using System.Linq;
using Xunit;

namespace SharePaint.Tests
{
    public class PaintTest
    {
        [Fact]
        public void ShouldCompletePaintTest()
        {
            //arrenge
            IUserDataService userDataService = new UserDataServiesProvider();
            IStrokeDataService strokeDataService = new StrokeDataServiesProvider();
            ILineDataService lineDataService = new LineDataServiesProvider();


            IPaintDataService paintDataService = new PaintDataServiesProvider();

            IUserServies userServies = new UserServiceProvider(userDataService);
            IPaintServies paintService = new PaintServiceProvider(paintDataService, userDataService, strokeDataService,lineDataService);
            IStrokeService strokeService = new StrokeServiceProvider(strokeDataService, userDataService, lineDataService);
            ILineService lineService = new LineServiceProvider(lineDataService, userDataService);

            //act
            var user = userServies.CreateUser();
            var paint = paintService.CreateOrGetPaint(user.Id);
            var stroke = strokeService.CreateStroke(new Stroke
            {
                PaintId = paint.Id,
                StartPoint = new Point { X = 1, Y = 1 }
            });
            var line = lineService.CreateLine(new Line
            {
                StrokeId = stroke.Id,
                StartPoint = new Point { X = 10, Y = 10 }
            });

            var paintResult = paintService.CreateOrGetPaint(user.Id);

            //assert
            Assert.Single(paintResult.Strokes);
            Assert.Single(paintResult.Strokes.FirstOrDefault().Lines);


        }
    }
}
