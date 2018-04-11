using System;
using System.Threading.Tasks;
using AndrewLarsson.CircleOfTrust.Domain.AggregateRoots;
using AndrewLarsson.CircleOfTrust.Domain.Exceptions;
using AndrewLarsson.CircleOfTrust.Domain.Repositories;

namespace AndrewLarsson.CircleOfTrust.Domain.Rules {
	public class CirclesMustHaveAUniqueNameRule {
		private readonly ICircleRepository _circleRepository;

		public CirclesMustHaveAUniqueNameRule(ICircleRepository circleRepository) {
			_circleRepository = circleRepository;
		}

		public async Task Verify(string name) {
			Circle circle = await _circleRepository.FindCircleByName(name);
			if (circle != null) {
				throw new CirclesMustHaveAUniqueNameException();
			}
		}

		public async Task Verify(Guid circleId, string name) {
			Circle circle = await _circleRepository.FindCircleByName(name);
			if (circle != null && circle.Id != circleId) {
				throw new CirclesMustHaveAUniqueNameException();
			}
		}
	}
}
