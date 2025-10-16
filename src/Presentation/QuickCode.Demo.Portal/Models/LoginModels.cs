using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace QuickCode.Demo.Portal.Models
{
	public class LoginData
	{
		[Required(ErrorMessage = "Username/Email is required")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Username { get; set; }
		
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
		
		public bool RememberMe { get; set; }
		public string ReturnUrl { get; set; }
		public string ErrorMessage { get; set; }
	}

	public class RegisterData
	{
		[Required(ErrorMessage = "First name is required")]
		[StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last name is required")]
		[StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }

		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}

	public class ForgotPasswordData
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}

	public class ResetPasswordData
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Reset code is required")]
		public string ResetCode { get; set; }

		[Required(ErrorMessage = "New password is required")]
		[StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm password is required")]
		[Compare("NewPassword", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }

		public string ErrorMessage { get; set; }
		public string SuccessMessage { get; set; }
	}
}

