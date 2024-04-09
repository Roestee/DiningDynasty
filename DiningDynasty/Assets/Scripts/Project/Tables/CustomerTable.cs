using System.Linq;
using UnityEngine;

namespace Project.Tables
{
    public class CustomerTable : MonoBehaviour
    {
        [SerializeField] private Chair[] chairs;

        public bool IsThereAvailableChair() => chairs.Any(p => p.CurrentCustomer == null);
        public Chair GetAvailableChair() => chairs.FirstOrDefault(p => p.CurrentCustomer == null);
    }
}