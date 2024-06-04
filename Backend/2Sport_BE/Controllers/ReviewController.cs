using _2Sport_BE.Repository.Interfaces;
using _2Sport_BE.Repository.Models;
using _2Sport_BE.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2Sport_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IReviewService _reviewService;

		public ReviewController(IUnitOfWork unitOfWork, IReviewService reviewService)
		{
			_unitOfWork = unitOfWork;
			_reviewService = reviewService;
		}

		[HttpPost]
		[Route("add-review")]
		public async Task<IActionResult> AddReview([FromQuery] int productId, [FromQuery] decimal star, 
												   [FromQuery] string review)
		{
			try
			{
				var userId = GetCurrentUserIdFromToken();

				if (userId == 0)
				{
					return Unauthorized();
				}
				var addedReview = new Review
				{
					Star = star,
					Review1 = review,
					Status = true,
					UserId = userId,
					ProductId = productId
				};
				await _reviewService.AddReview(addedReview);
				if (await _unitOfWork.SaveChanges())
				{
					return Ok(addedReview);
				} else
				{
					return BadRequest("Added review failed!");
				}
			} catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		protected int GetCurrentUserIdFromToken()
		{
			int UserId = 0;
			try
			{
				if (HttpContext.User.Identity.IsAuthenticated)
				{
					var identity = HttpContext.User.Identity as ClaimsIdentity;
					if (identity != null)
					{
						IEnumerable<Claim> claims = identity.Claims;
						string strUserId = identity.FindFirst("UserId").Value;
						int.TryParse(strUserId, out UserId);

					}
				}
				return UserId;
			}
			catch
			{
				return UserId;
			}
		}
	}
}
