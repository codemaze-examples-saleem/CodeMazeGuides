﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenAiImageGenerator;
using OpenAiImageGenerator.Controllers;
using OpenAiImageGenerator.Services;
using System.Net;

namespace Tests;

public class ImageGeneratorControllerTests
{
    [Fact]
    public void GivenImageGeneratorController_WhenInvoked_ThenGiveImageURL()
    {
        // Arrange
        var openAIServiceMock = new Mock<IOpenAIService>();
        var imageGenerationApiModel = new ImageGenerationApiModel
        {
            Prompt = "A painting of a beautiful sunset over a mountain",
            Size = "256x256",
            Style = "natural",
            Quality = "standard"
        };
        var imageUri = new Uri("https://www.example.com/image.jpg");
        
        openAIServiceMock
            .Setup(x => x.GenerateImageAsync(imageGenerationApiModel))
            .ReturnsAsync(imageUri);

        var controller = new ImageGeneratorController(openAIServiceMock.Object);

        // Act
        var result = controller.PostAsync(imageGenerationApiModel).Result as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        Assert.Equal(imageUri, result.Value);
    }
}