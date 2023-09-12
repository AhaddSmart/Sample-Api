using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BannerDto
{
    public class BannerWithFileUrlDto
    {
        public Banner banner { get; set; }
        public string fileUrl { get; set; }
    }
}
