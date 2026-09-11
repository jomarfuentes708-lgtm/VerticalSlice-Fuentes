namespace Application.Features.Admissions.GetAllAdmissions;

using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

public sealed class GetAllAdmissionsQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetAllAdmissionsQuery, Result<GetAllAdmissionsResponse>>
{
    public async Task<Result<GetAllAdmissionsResponse>> HandleAsync(
        GetAllAdmissionsQuery query,
        CancellationToken cancellationToken = default)
    {
        var admissions = await dbContext.Admissions
            .Include(a => a.Student)
            .Include(a => a.AdmissionCourses)
            .Select(a => new AdmissionDto(
                a.Id,
                a.StudentId,
                $"{a.Student.FirstName} {a.Student.LastName}",
                a.AcademicYear, 
                a.AdmissionDate,
                a.AdmissionCourses.Count
            ))
            .ToListAsync(cancellationToken);

        var response = new GetAllAdmissionsResponse(admissions);
        return Result<GetAllAdmissionsResponse>.Success(response);
    }
}
