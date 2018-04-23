using Newtonsoft.Json;
using SharePaint.Data.Models;
using ShareStroke.Services.ServiceProviders;
using System;

namespace SharePaint.Services.ServiceProviders
{
    public class ReceiveDataHandler : IReceiveDataHandler
    {
        private readonly ILineService _lineService;
        private readonly IStrokeService _strokeService;


        public ReceiveDataHandler(ILineService lineService, IStrokeService strokeService)
        {
            _lineService = lineService;
            _strokeService = strokeService;
        }


        public ( bool IsSucess, bool IsRefresh)Handle(string receivedData)
        {
            if(!string.IsNullOrEmpty(receivedData))
            {
                try
                {
                    var line = JsonConvert.DeserializeObject<Line>(receivedData);
                    if(line.StrokeId != Guid.Empty && line.StartPoint != null && line.StrokeId != Guid.Empty)
                    {
                        //should be done async when adding to db
                        _lineService.CreateLine(line);
                        return (true,false);
                       
                    }
                    var stroke = JsonConvert.DeserializeObject<Stroke>(receivedData);
                    if (stroke.Id != Guid.Empty || stroke.StartPoint != null)
                    {
                         _strokeService.Update(stroke);
                        return (true,true);
                    }

                        return (false,false);


                }
                catch(JsonException ex)
                {
                    return (false, false);
                }

            }
            return (false, false);

        }
    }
}
