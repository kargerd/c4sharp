namespace C4Sharp.Diagrams.Supplementary
{
    /// <summary>
    /// A dynamic diagram can be useful when you want to show how elements in a static model collaborate at runtime 
    /// to implement a user story, use case, feature, etc. This dynamic diagram is based upon a UML communication 
    /// diagram (previously known as a "UML collaboration diagram"). It is similar to a UML sequence diagram although
    /// it allows a free-form arrangement of diagram elements with numbered interactions to indicate ordering.
    /// <see href="https://c4model.com/#DynamicDiagram"/>
    /// </summary>
    public record DynamicDiagram() : Diagram(DiagramType.Dynamic)
    {
        /// <summary>
        /// Returns current index and calculates next index
        /// </summary>
        public static int Index(int offset = 1)
        {
            _ = offset;

            return 0;
        }

        /// <summary>
        /// Returns new set index and calculates next index
        /// </summary>
        public static int SetIndex(int newIndex, int offset = 1)
        {
            _ = newIndex;
            _ = offset;

            return 0;
        }

        /// <summary>
        /// return the last used index
        /// </summary>
        public static int LastIndex() => 0;
    }
}
