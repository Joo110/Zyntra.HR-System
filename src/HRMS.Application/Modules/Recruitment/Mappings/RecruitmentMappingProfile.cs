using AutoMapper; using HRMS.Application.Modules.Recruitment.DTOs; using HRMS.Domain.Entities;
namespace HRMS.Application.Modules.Recruitment.Mappings;
public class RecruitmentMappingProfile : Profile
{
    public RecruitmentMappingProfile()
    {
        CreateMap<JobVacancy, JobVacancyDto>().ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null));
        CreateMap<Candidate, CandidateDto>().ForMember(d => d.JobTitle, o => o.MapFrom(s => s.JobVacancy != null ? s.JobVacancy.Title : null));
        CreateMap<Interview, InterviewDto>().ForMember(d => d.CandidateName, o => o.MapFrom(s => s.Candidate != null ? $"{s.Candidate.FirstName} {s.Candidate.LastName}" : null));
    }
}
