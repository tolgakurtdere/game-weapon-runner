using System;
using UnityEngine;

namespace WeaponRunner
{
    public static class EconomyManager
    {
        private const string COIN_COUNT_KEY = "com.tk.coinCount";

        public static event Action<int> OnCoinCountChanged;

        private static int? _coinCount;

        public static int CoinCount
        {
            get
            {
#if TEST_MODE
                _coinCount ??= PlayerPrefs.GetInt(COIN_COUNT_KEY, 999);
#endif
                _coinCount ??= PlayerPrefs.GetInt(COIN_COUNT_KEY, 0);
                return _coinCount.Value;
            }
            private set
            {
                if (_coinCount == value)
                {
                    return;
                }

                _coinCount = value;
                PlayerPrefs.SetInt(COIN_COUNT_KEY, value);

                OnCoinCountChanged?.Invoke(value);
            }
        }

        public static bool AddOrSubtractCoin(int amount)
        {
            if (CoinCount + amount < 0)
            {
                return false;
            }

            CoinCount += amount;
            return true;
        }
    }
}