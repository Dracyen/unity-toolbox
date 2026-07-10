using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputProfile
{
    public event Action PressDirectionDown;
    public event Action HoldDirectionDown;
    public event Action ReleaseDirectionDown;

    public event Action PressDirectionUp;
    public event Action HoldDirectionUp;
    public event Action ReleaseDirectionUp;

    public event Action PressDirectionLeft;
    public event Action HoldDirectionLeft;
    public event Action ReleaseDirectionLeft;

    public event Action PressDirectionRight;
    public event Action HoldDirectionRight;
    public event Action ReleaseDirectionRight;
}
