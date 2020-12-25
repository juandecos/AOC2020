namespace AOC2020
{
    [Day(25)]
    class Day25 : Solver
    {
        public override object SolveOne()
        {
            int cardPublicKey = int.Parse(Rows[0]), doorPublicKey = int.Parse(Rows[1]);
            int cardPublicKeyAttempt = StepTransform(7, 1);
            int encryptionKey = 1;
            while (cardPublicKeyAttempt != cardPublicKey)
            {
                cardPublicKeyAttempt = StepTransform(7, cardPublicKeyAttempt);
                encryptionKey = StepTransform(doorPublicKey, encryptionKey);
            }
            return StepTransform(doorPublicKey, encryptionKey);
        }

        int StepTransform(int subjectNumber, int lastValue) => (int)(((long)lastValue * subjectNumber) % 20201227);

        public override object SolveTwo()
        {
            return "Merry Christmas!";
        }
    }
}
