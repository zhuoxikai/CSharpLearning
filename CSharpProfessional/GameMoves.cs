using System;
using System.Collections;

namespace CSharpProfessional
{
    public class GameMoves
    {
        private IEnumerator _circle;
        private IEnumerator _cross;
        

        private int _move = 0;

        private const int MaxMoves = 9;

        public GameMoves()
        {
            _circle = Circle();
            _cross = Cross();
            
        }
        public IEnumerator Circle()
        {
            while (true)
            {
                Console.WriteLine($"Circle, move {_move}");
                if (++ _move >= MaxMoves)
                {
                    yield break;
                }

                yield return _cross;
            }
        }
        public IEnumerator Cross()
        {
            while (true)
            {
                Console.WriteLine($"Cross, move {_move}");
                if (++ _move >= MaxMoves)
                {
                    yield break;
                }

                yield return _circle;
            }
        }
        
        
    }
}