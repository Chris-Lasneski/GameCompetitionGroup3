using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    // quest givers name
    public Text questGiver;
    //main body of text in ui
    public Text questDescription;
    // get the stage of the quest, ie begin or end
    public bool beginning = true;

    // these are place holders currently idk how we want to approach these.
    enum names {Bob, Tim, Frank, Sally, Sue};

    // Start is called before the first frame update
    void Start()
    {
        questGiver.text = "place holder";
        questDescription.text = "Description";
    }

    // Update is called once per frame
    void Update()
    {
        // at this point we will grab the quest givers name from a list,still need to implement
        questGiver.text = names.Bob.ToString();
        if (beginning)
        {
            // grab associated text with name and stage
            questDescription.text = "Lorem ipsum dolor sit amet. Ad voluptatum molestiae est eveniet placeat ut animi fuga sit dolorem reprehenderit. Ut voluptatem voluptas et voluptatem nihil At voluptatibus officiis ea repudiandae quisquam ut nulla reprehenderit non atque ipsum.\r\n\r\n\r\nEst reiciendis dicta aut facere recusandae quo omnis tempore aut voluptatum error qui consequatur nihil ut doloremque quas vel facere itaque. Non officiis consequatur est sunt mollitia aut totam voluptas. Et debitis quia qui magnam laudantium cum rerum deleniti.\r\n\r\n\r\nA veritatis voluptatem ab nobis repudiandae qui dolor labore. Aut maxime repudiandae aut repudiandae debitis qui porro rerum vel neque inventore eos nesciunt blanditiis.";
            
        }
        else
        {
            questDescription.text = "QUEST COMPLETED!";
        }
    }
}
