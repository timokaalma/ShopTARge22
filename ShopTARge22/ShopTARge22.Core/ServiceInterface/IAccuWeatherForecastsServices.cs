using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARge22.Core.DTO.AccuWeatherDTOs;

namespace ShopTARge22.Core.ServiceInterface
{
    public interface IAccuWeatherForecastsServices
    {
        Task<AccuWeatherLocationResultDTO> AccuWeatherGet(AccuWeatherLocationResultDTO dtoLocation);
        Task<AccuWeatherResultDTO> AccuWeatherResult(AccuWeatherResultDTO dto, AccuWeatherLocationResultDTO dtoLocation);
    }
}