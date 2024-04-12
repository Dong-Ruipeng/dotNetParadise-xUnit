using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sample.Repository.Entities;
using Sample.Repository.Repositories;

namespace Sample.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IStaffRepository staffRepository) : ControllerBase
{
    private readonly IStaffRepository _staffRepository = staffRepository;

    [HttpPost]
    public async Task<Results<NoContent, ProblemHttpResult>> AddStaff([FromBody] Staff staff, CancellationToken cancellationToken = default)
    {
        try
        {
            await _staffRepository.AddStaffAsync(staff, cancellationToken);
            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Detail = ex.Message,
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteStaff(int id, CancellationToken cancellationToken = default)
    {
        await _staffRepository.DeleteStaffAsync(id);
        return TypedResults.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<Results<BadRequest<string>, NoContent, NotFound>> UpdateStaff(int id, [FromBody] Staff staff, CancellationToken cancellationToken = default)
    {
        if (id != staff.Id)
        {
            return TypedResults.BadRequest("Staff ID mismatch");
        }
        var originStaff = await _staffRepository.GetStaffByIdAsync(id, cancellationToken);
        if (originStaff is null) return TypedResults.NotFound();
        originStaff.Update(staff);
        await _staffRepository.UpdateStaffAsync(originStaff, cancellationToken);
        return TypedResults.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<Results<Ok<Staff>, NotFound>> GetStaffById(int id, CancellationToken cancellationToken = default)
    {
        var staff = await _staffRepository.GetStaffByIdAsync(id, cancellationToken);
        if (staff == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(staff);
    }


    [HttpGet]
    public async Task<IResult> GetAllStaff(CancellationToken cancellationToken = default)
    {
        var staffList = await _staffRepository.GetAllStaffAsync(cancellationToken);
        return TypedResults.Ok(staffList);
    }


    [HttpPost("BatchAdd")]
    public async Task<IResult> BatchAddStaff([FromBody] List<Staff> staffList, CancellationToken cancellationToken = default)
    {
        await _staffRepository.BatchAddStaffAsync(staffList, cancellationToken);
        return TypedResults.NoContent();
    }

}
