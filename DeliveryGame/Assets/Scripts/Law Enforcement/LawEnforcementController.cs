using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawEnforcementController
{
    public static void reportTicket(float speed) {
        Debug.Log($"Ticket for going {speed} Mph reported by camera");
    }
}
