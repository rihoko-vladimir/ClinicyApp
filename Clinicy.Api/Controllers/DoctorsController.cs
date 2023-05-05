using AutoMapper;
using Clinicy.WebApi.Extensions.ControllerExtensions;
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
    private readonly IAccessTokenService _accessTokenService;

    public DoctorsController(IDoctorsService doctorsService, IMapper mapper, IAccessTokenService accessTokenService)
    {
        _doctorsService = doctorsService;
        _mapper = mapper;
        _accessTokenService = accessTokenService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);


        if (!success) return BadRequest();
        var allDoctors = await _doctorsService.GetAllDoctors();

        return Ok(_mapper.Map<IEnumerable<DoctorResponse>>(allDoctors));

    }

    [HttpGet]
    [Route("findById")]
    public async Task<IActionResult> GetDoctorById([FromQuery] string doctorId)
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);

        if (!success) return BadRequest();
        var doctor = await _doctorsService.GetDoctorById(Guid.Parse(doctorId));

        return Ok(_mapper.Map<DoctorResponse>(doctor));

    }

    [HttpGet]
    [Route("findByCriteria")]
    public async Task<IActionResult> GetDoctorsByCriteria([FromQuery] string? firstName, [FromQuery] string? lastName,
        [FromQuery] string? parentsName, [FromQuery] string? qualification)
    {
        var token = await this.GetAccessTokenAsync();
        var success = _accessTokenService.GetGuidFromAccessToken(token, out _);

        if (!success) return BadRequest();
        var doctors = await _doctorsService.FindDoctorsByCriteria(firstName, lastName, parentsName, qualification);

        return Ok(_mapper.Map<IEnumerable<DoctorResponse>>(doctors));

    }
}