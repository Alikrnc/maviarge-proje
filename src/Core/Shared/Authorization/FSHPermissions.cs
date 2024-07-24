using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace FSH.WebApi.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string ViewMy = nameof(ViewMy);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string UpdateMy = nameof(UpdateMy);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
    public const string SendPushNotifications = nameof(SendPushNotifications);
    public const string SendSms = nameof(SendSms);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Interviews = nameof(Interviews);
    public const string Applications = nameof(Applications);
    public const string Brands = nameof(Brands);
    public const string JobPostings = nameof(JobPostings);
    public const string CandidateInfos = nameof(CandidateInfos);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),
        new("Send Push Notifications to User", FSHAction.SendPushNotifications, FSHResource.Users),
        new("Send Sms to Users", FSHAction.SendSms, FSHResource.Users),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),
        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),
        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Applications", FSHAction.View, FSHResource.Applications, IsBasic: true),
        new("Search Applications", FSHAction.Search, FSHResource.Applications, IsBasic: true),
        new("Create Applications", FSHAction.Create, FSHResource.Applications, IsBasic: true),
        new("Update Applications", FSHAction.Update, FSHResource.Applications, IsBasic: true),
        new("Delete Applications", FSHAction.Delete, FSHResource.Applications, IsBasic: true),
        new("Export Applications", FSHAction.Export, FSHResource.Applications),
        new("View Interviews", FSHAction.View, FSHResource.Interviews, IsBasic: true),
        new("Search Interviews", FSHAction.Search, FSHResource.Interviews, IsBasic: true),
        new("Create Interviews", FSHAction.Create, FSHResource.Interviews),
        new("Update Interviews", FSHAction.Update, FSHResource.Interviews),
        new("Delete Interviews", FSHAction.Delete, FSHResource.Interviews),
        new("Export Interviews", FSHAction.Export, FSHResource.Interviews),
        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("View Job Postings", FSHAction.View, FSHResource.JobPostings, IsBasic: true),
        new("Search Job Postings", FSHAction.Search, FSHResource.JobPostings, IsBasic: true),
        new("Create Job Postings", FSHAction.Create, FSHResource.JobPostings),
        new("Update Job Postings", FSHAction.Update, FSHResource.JobPostings),
        new("Delete Job Postings", FSHAction.Delete, FSHResource.JobPostings),
        new("Generate Job Postings", FSHAction.Generate, FSHResource.JobPostings),
        new("Clean Job Postings", FSHAction.Clean, FSHResource.JobPostings),
        new("View Candidate Informations", FSHAction.View, FSHResource.CandidateInfos, IsBasic: true),
        new("Search Candidate Informations", FSHAction.Search, FSHResource.CandidateInfos, IsBasic: true),
        new("Create Candidate Informations", FSHAction.Create, FSHResource.CandidateInfos),
        new("Update Candidate Informations", FSHAction.Update, FSHResource.CandidateInfos),
        new("Delete Candidate Informations", FSHAction.Delete, FSHResource.CandidateInfos),
        new("Generate Candidate Informations", FSHAction.Generate, FSHResource.CandidateInfos),
        new("Clean Candidate Informations", FSHAction.Clean, FSHResource.CandidateInfos),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Update My Tenant's Details", FSHAction.UpdateMy, FSHResource.Tenants),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true),
        new("View my Tenant", FSHAction.ViewMy, FSHResource.Tenants),
    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}