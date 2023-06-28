public class WeaponStatus
{
    public int maxAmmo;
    public int currentAmmo;
    public bool isReloading;

    public WeaponStatus(int maxAmmo, int currentAmmo, bool isReloading)
    {
        this.maxAmmo = maxAmmo;
        this.currentAmmo = currentAmmo;
        this.isReloading = isReloading;
    }
}
