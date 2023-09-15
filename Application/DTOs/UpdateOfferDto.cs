
using System;
using Application.Common.Mappings;
using Domain.Entities;
using AutoMapper;
using Domain.Enums;
using System.Reflection.Metadata;



namespace Application.DTOs
{
    public class UpdateOfferDto
    {
    public int id { get; set; }
public string title { get; set; }
public int? fileRepoId { get; set; }
public DateTime? to { get; set; }
public DateTime? from { get; set; }
                        

    }
 }
            