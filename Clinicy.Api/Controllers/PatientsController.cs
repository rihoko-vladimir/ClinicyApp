using AutoMapper;
using Clinicy.WebApi.Interfaces.Services;
using Clinicy.WebApi.Models;
using Clinicy.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Clinicy.WebApi.Controllers;

[ApiController]
[Route("/api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPatientsService _patientsService;

    public PatientsController(IMapper mapper, IPatientsService patientsService)
    {
        _mapper = mapper;
        _patientsService = patientsService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllPatients()
    {
        var allPatients = await _patientsService.GetAllPatients();

        return Ok(_mapper.Map<IEnumerable<PatientResponse>>(allPatients));
    }

    [HttpGet]
    [Route("findById")]
    public async Task<IActionResult> GetPatientById([FromQuery] string patientId)
    {
        var patient = await _patientsService.GetPatientById(Guid.Parse(patientId));

        return Ok(_mapper.Map<PatientResponse>(patient));
    }

    [HttpGet]
    [Route("findByCriteria")]
    public async Task<IActionResult> GetPatientsByCriteria([FromQuery] string? firstName, [FromQuery] string? lastName,
        [FromQuery] string? passportNumber,
        [FromQuery] string? email,
        [FromQuery] char? gender)
    {
        var patients = await _patientsService.FindPatientsByCriteria(firstName, lastName, passportNumber, email,
            GenderExtensions.ParseCharToGender(gender));

        return Ok(_mapper.Map<IEnumerable<PatientResponse>>(patients));
    }
}