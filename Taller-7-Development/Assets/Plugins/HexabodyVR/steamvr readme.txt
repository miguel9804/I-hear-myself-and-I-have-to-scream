If you're using hexabody with hurricanevr, you don't need the steamvr integration included with this package, so delete the folder if you extracted it

If you're using this standalone and want to use steamvr inputs:
1. install steamvr asset first
2. accept the bindings import prompt
3. generate the bindings in the SteamVR Input window included with steamvr asset
4. Add HEXA_STEAMVR define symbtol to your player settings, otherwise the code using the actions will not compile and you will not have inputs

