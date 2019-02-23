/*
Ported from Go to C#

Original: https://github.com/fogleman/nes/blob/master/nes/controller.go

------------------------------------------------------------------------

Copyright (C) 2015 Michael Fogleman

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the "Software"), to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 */


namespace NES
{
    class Controller
    {
        public Controller() {
            Press(ControllerKey.Down);
        }

        public bool[] pressed = new bool[8];
        private byte index;
        private byte strobe;

        public void Press(ControllerKey key)
        {
            pressed[(int)key] = true;
        }

        public void Release(ControllerKey key)
        {
            pressed[(int)key] = false;
        }

        public void SetKey(ControllerKey key, bool value)
        {
            pressed[(int)key] = value;
        }

        public byte Get()
        {
            byte value = 0;
            if (index < 8 && pressed[index])
            {
                value = 1;

            }
            index++;

            if ((strobe & 1) == 1)
            {
                index = 0;
            }
            return value;
        }

        public void Set(byte value)
        {
            strobe = value;

            if ((strobe & 1) == 1) index = 0;
        }
    }
}