﻿using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace UpWorker.Core.Models;

// Model for the SampleDataService. Replace with your own model.
public class JobListing
{

    private List<string> skills;

    public string Title
    {
        get; set;
    }

    public string Category
    {
        get; set;
    }

    public string PostedOn
    {
        get; set; 
    }

    public List<string> Skills
    {
        get => skills; set => skills = value;
    }

    public string LocationRequirement
    {
        get; set;
    }

    public string Country
    {
        get; set;
    }

    public string Payment
    {
        get; set;
    }

    public string Link
    {
        get; set;
    }

    public bool Notified
    {
        get; set;
    }

    public string InsertDateTime
    {
        get; set;
    }

    public string SearchUrl
    {
        get; set;
    }

    public string SymbolName
    {
        get; set;
    }

    public int SymbolCode
    {
        get; set;
    }

    public string ShortDescription => $"{Title} - {Payment}";
    public string SkillList => string.Join(", ", Skills);
    public char Symbol => (char)SymbolCode;
    public string notificationPayload => $"<toast launch=\"action=ToastClick&amp;url={Link}\"><visual><binding template=\"ToastGeneric\"><text>{Title}</text><text>{Payment}</text><text>Check the app for more details.</text></binding></visual></toast>";
}
