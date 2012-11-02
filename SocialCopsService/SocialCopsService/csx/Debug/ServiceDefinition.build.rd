<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="SocialCopsService" generation="1" functional="0" release="0" Id="8ba6c2e2-3ce4-4d10-aeb6-2966a8d84de5" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="SocialCopsServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="CoreService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/SocialCopsService/SocialCopsServiceGroup/LB:CoreService:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="CoreService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SocialCopsService/SocialCopsServiceGroup/MapCoreService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="CoreService:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/SocialCopsService/SocialCopsServiceGroup/MapCoreService:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="CoreServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/SocialCopsService/SocialCopsServiceGroup/MapCoreServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:CoreService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreService/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCoreService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCoreService:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreService/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapCoreServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CoreService" generation="1" functional="0" release="0" software="C:\Users\Banka\Desktop\GitHub\socialCopsDev\SocialCopsService\SocialCopsService\csx\Debug\roles\CoreService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CoreService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CoreService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="CoreService.svclog" defaultAmount="[1000,1000,1000]" defaultSticky="true" kind="Directory" />
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreServiceInstances" />
            <sCSPolicyFaultDomainMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyFaultDomain name="CoreServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="CoreServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="f684d877-837c-4e47-98a5-4891d1ffbf3e" ref="Microsoft.RedDog.Contract\ServiceContract\SocialCopsServiceContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="9d0c7029-5c15-4bcf-b30e-c6c50cf89f2a" ref="Microsoft.RedDog.Contract\Interface\CoreService:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/SocialCopsService/SocialCopsServiceGroup/CoreService:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>