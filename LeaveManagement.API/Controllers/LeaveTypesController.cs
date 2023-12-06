using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Logging;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<LeaveTypesController> _logger;

        public LeaveTypesController(IMediator mediator, IAppLogger<LeaveTypesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());

            return leaveTypes;
        }

        // GET: api/LeaveTypesDetails/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<LeaveTypeDetailDto>> Get(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));

            return Ok(leaveType);
        }

        // POST: api/LeaveTypesController
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateLeaveTypeCommand commandType)
        {
            try
            {
                var leaveTypeId = await _mediator.Send(commandType);
                return CreatedAtAction(nameof(Get), new { id = leaveTypeId });
            }
            catch (BadRequestException e)
            {
                _logger.LogError(e.ValidationErrors.ToString() ?? "", e.ValidationErrors);
                return BadRequest(e.ValidationErrors.ToString() ?? "");
            }
        }

        // PUT: api/LeaveTypesController
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put([FromBody] UpdateLeaveTypeCommand commandType)
        {
            try
            {
                await _mediator.Send(commandType);
                return NoContent();
            }
            catch (BadRequestException e)
            {
                _logger.LogError(e.Message, e.ValidationErrors);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteLeaveTypeCommand(id));
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return NotFound("LeaveType not found");
            }
        }
    }
}