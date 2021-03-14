﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using PaderConference.Core.Services.BreakoutRooms;
using PaderConference.Core.Services.BreakoutRooms.Internal;
using PaderConference.Core.Services.BreakoutRooms.Requests;
using PaderConference.Core.Services.BreakoutRooms.UseCases;
using PaderConference.Tests.Utils;
using Xunit;

namespace PaderConference.Core.Tests.Services.BreakoutRooms.UseCases
{
    public class CloseBreakoutRoomsUseCaseTests
    {
        private const string ConferenceId = "123";
        private readonly Mock<IMediator> _mediator = new();

        private CloseBreakoutRoomsUseCase Create()
        {
            return new(_mediator.Object);
        }

        [Fact]
        public async Task Handle_DefaultRequest_ApplyBreakoutRoomRequest()
        {
            // arrange
            var capturedRequest = _mediator.CaptureRequest<ApplyBreakoutRoomRequest, BreakoutRoomInternalState?>();
            var useCase = Create();
            var request = new CloseBreakoutRoomsRequest(ConferenceId);

            // act
            await useCase.Handle(request, CancellationToken.None);

            // assert
            capturedRequest.AssertReceived();

            var applyRequest = capturedRequest.GetRequest();
            Assert.Equal(ConferenceId, applyRequest.ConferenceId);
            Assert.Null(applyRequest.State);
        }
    }
}
