﻿using System;
using Api.Models;
using Api.Services.AddressValidation;
using Api.Services.Config;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressValidationService addressValidationService;
        private readonly IConfigService configService;

        public AddressController(IAddressValidationService addressValidationService, IConfigService configService)
        {
            this.addressValidationService = addressValidationService;
            this.configService = configService;
        }

        [HttpPost]
        [Route("isvalid")]
        public IActionResult IsValid([FromBody] Address address)
        {
            try
            {
                return Ok(new { IsValid = addressValidationService.IsValid(address) });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message});
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult AddAddressesFormat([FromBody] RegexAddressFormat regexAddressFormat)
        {
            try
            {
                configService.AddRegexAddressFormat(regexAddressFormat);   
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPut]
        [Route("")]
        public IActionResult EditAddressesFormat([FromBody] RegexAddressFormat regexAddressFormat)
        {
            try
            {
                configService.EditRegexAddressFormat(regexAddressFormat);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("")]
        public IActionResult Delete([FromQuery] string countryCode)
        {
            try
            {
                configService.DeleteRegexAddressFormat(countryCode);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
