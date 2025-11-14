namespace _Scripts
{
    public class BoxItem
    {
        private JamsType jamsType;


        public BoxItem(JamsType jamsType)
        {
            this.jamsType = jamsType;
        }
        
        public JamsType GetJamsType()
        {
            return jamsType;
        }
        
        public bool isEqualTo(BoxItem otherBox)
        {
            return this.jamsType == otherBox.jamsType;
        }
    }
    
    
    public enum JamsType
    {
        Empty,
        Strawberry,
        Blueberry,
        Eyeballs,
        Blackberry,
        Slime,
        Pineapple,
        Broken,
    }
}