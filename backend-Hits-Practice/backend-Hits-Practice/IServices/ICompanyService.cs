using Events.innerModels;
using Events.requestsModels;
using Events.responseModels;

namespace Events.Interfaces;

public interface ICompanyService
{
    Task<CompaniesListResponseModel> GetCompaniesListAsync(string token);
    Task<CompaniesNamesResponseModel> GetCompaniesNamesAsync(string name);
    Task<bool> AddCompany(string token, CompanyCreateModel company);
    Task<bool> DeleteCompany(string token, CompanyDeleteModel company);
}