using AutoMapper;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Clinicy.WebApi.Controllers;

[ApiController]
[Route("/api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsService _doctorsService;
    private readonly IMapper _mapper;

    public DoctorsController(IDoctorsService doctorsService, IMapper mapper)
    {
        _doctorsService = doctorsService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var allDoctors = await _doctorsService.GetAllDoctors();

        return Ok(_mapper.Map<IEnumerable<DoctorResponse>>(allDoctors));
    }

    [HttpGet]
    [Route("findById")]
    public async Task<IActionResult> GetDoctorById([FromQuery] string doctorId)
    {
        var doctor = await _doctorsService.GetDoctorById(Guid.Parse(doctorId));

        return Ok(_mapper.Map<DoctorResponse>(doctor));
    }

    [HttpGet]
    [Route("findByCriteria")]
    public async Task<IActionResult> GetDoctorsByCriteria([FromQuery] string? firstName, [FromQuery] string? lastName,
        [FromQuery] string? parentsName, [FromQuery] string? qualification)
    {
        var doctors = await _doctorsService.FindDoctorsByCriteria(firstName, lastName, parentsName, qualification);

        return Ok(_mapper.Map<IEnumerable<DoctorResponse>>(doctors));
    }
}