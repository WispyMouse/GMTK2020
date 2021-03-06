﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OperationHandler
{
    public Sprite ThumbsUpSprite;

    public Action<string, string, Sprite> CustomNotificationCallback;
    public Action<GameResource> ResourceNotificationCallback;
    public Action<GameResource> ResourceAddedCallback;

    public void Initiate(
        Sprite thumbsUpSprite,
        Action<string, string, Sprite> customNotificationCallback,
        Action<GameResource> resourceNotificationCallback,
        Action<GameResource> resourceAddedCallback)
    {
        ThumbsUpSprite = thumbsUpSprite;
        CustomNotificationCallback = customNotificationCallback;
        ResourceNotificationCallback = resourceNotificationCallback;
        ResourceAddedCallback = resourceAddedCallback;
    }
}
