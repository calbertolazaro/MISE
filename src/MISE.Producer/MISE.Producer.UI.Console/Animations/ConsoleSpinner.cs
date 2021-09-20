namespace MISE.Producer.UI.Console.Animations
{
    /// <summary>
    /// Author: Jason Roberts
    /// Title: http://dontcodetired.com/blog/post/Creating-a-Spinner-Animation-in-a-Console-Application-in-C
    /// </summary>
    internal class ConsoleSpinner
    {
        private int _currentAnimationFrame;

        internal ConsoleSpinner()
        {
            SpinnerAnimationFrames = new[]
            {
                '|',
                '/',
                '-',
                '\\'
            };
        }
        
        public char[] SpinnerAnimationFrames { get; set; }

        public void UpdateProgress()
        {
            // Store the current position of the cursor
            var originalX = System.Console.CursorLeft;
            var originalY = System.Console.CursorTop;

            // Write the next frame (character) in the spinner animation
            System.Console.Write(SpinnerAnimationFrames[_currentAnimationFrame]);

            // Keep looping around all the animation frames
            _currentAnimationFrame++;
            if (_currentAnimationFrame == SpinnerAnimationFrames.Length)
            {
                _currentAnimationFrame = 0;
            }

            // Restore cursor to original position
            System.Console.SetCursorPosition(originalX, originalY);
        }
    }
}
