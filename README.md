## How to use

#### Execute tool from ``cmd`` 

```
  Ipv4Filter --file-input <file path> --address-start <subnet bit>
```

| Parameter | Type     |                
| :-------- | :------- | 
| `--file-input` | **Required** |
| `--file-output` | **Optional** |
| `--address-start` | **Optional** | 
| `--address-end` | **Optional** |
| `--ftom-time` | **Optional** |
| `--to-time` | **Optional** |

#### Output File example 


```
{
  "filterOptios": {
    "fromIpAddress": "128.0.0.0",
    "toIpAddress": "255.255.255.255"
  },
  "logs": [
    {
      "ipAddress": "101.202.30.45",
      "count": 2
    },
    {
      "ipAddress": "89.12.45.67",
      "count": 1
    },
    {
      "ipAddress": "42.56.78.123",
      "count": 1
    },
    {
      "ipAddress": "54.23.76.90",
      "count": 1
    },
    {
      "ipAddress": "98.76.54.32",
      "count": 3
    },
    {
      "ipAddress": "10.0.0.1",
      "count": 1
    },
    {
      "ipAddress": "10.10.10.1",
      "count": 1
    },
    {
      "ipAddress": "78.56.34.12",
      "count": 1
    }
  ]
}
```
