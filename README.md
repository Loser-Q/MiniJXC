# MiniJXC 简易进销存系统

> 基于 Admin.Core 单体服务模式构建的微型企业进销存管理系统

## 🚀 技术栈

| 层 | 技术 |
|------|------|
| **后端** | .NET 10 + FreeSql ORM + Autofac + Mapster + CAP |
| **前端** | Vue 3 + Element Plus + TypeScript + Vite |
| **数据库** | SQL Server（命名实例 `LJQ\SQLEXPRESS`，Windows集成认证） |
| **API文档** | Swagger（`/doc/admin/swagger`） |

## 📦 功能模块

| 模块 | 功能 | 状态 |
|------|------|------|
| **基础资料** | 商品管理、往来单位（客户/供应商）、仓库管理、结算账户 | ✅ 已完成 |
| **购货管理** | 购货单（CRUD + 审核联动库存）、以销定购 | ✅ 后端完成 |
| **销货管理** | 销货单（CRUD + 审核联动库存 + 库存不足校验） | ✅ 后端完成 |
| **库存管理** | 库存查询、盘点单（盘盈/盘亏）、调拨单（双仓） | ✅ 后端完成 |
| **资金管理** | 收款单（核销应收）、付款单（核销应付） | ✅ 后端完成 |
| **编码规则** | 单据编号自动生成 | ✅ 后端完成 |
| **进销存页面** | 购货单、销货单、收付款、库存、盘点、调拨 | ⏳ 待前端 |

## 🏃 启动

### 后端

```powershell
cd src\modules\admin\ZhonTai.Admin.Host
dotnet run
# 访问 http://localhost:18010
# API 文档 http://localhost:18010/doc/admin/swagger
# 默认账号 admin / 123asd
```

### 前端

```powershell
cd ui\zhontai.ui.admin.vue3
pnpm dev
# 访问前端页面
```

## 🗄️ 数据库

| 表名 | 说明 |
|------|------|
| `jxc_product` | 商品 |
| `jxc_contact` | 往来单位（客户/供应商合并） |
| `jxc_warehouse` | 仓库 |
| `jxc_account` | 结算账户 |
| `jxc_purchase` + `jxc_purchase_item` | 购货单 + 明细 |
| `jxc_sale` + `jxc_sale_item` | 销货单 + 明细 |
| `jxc_receipt` | 收款单 |
| `jxc_payment` | 付款单 |
| `jxc_inventory` | 库存流水 |
| `jxc_stock_check` + `jxc_stock_check_item` | 盘点单 + 明细 |
| `jxc_transfer` + `jxc_transfer_item` | 调拨单 + 明细 |
| `jxc_bill_code_rule` | 单据编码规则 |

## 📂 项目结构

```
MiniJXC/
├── src/
│   ├── platform/                        # 框架层
│   │   ├── ZhonTai.Common               # 通用工具
│   │   ├── ZhonTai.DynamicApi           # 动态API（自动Controller）
│   │   └── ZhonTai.ApiUI               # Swagger UI
│   ├── modules/
│   │   ├── admin/
│   │   │   ├── ZhonTai.Admin.Core       # 权限核心
│   │   │   ├── ZhonTai.Admin            # 业务服务层
│   │   │   └── ZhonTai.Admin.Host       # 🚀 启动项目
│   │   ├── business/
│   │   │   └── MiniJXC.Business         # 📦 进销存业务模块
│   │   │       ├── Entities/            # 11个实体类
│   │   │       ├── Services/            # 12个Service
│   │   │       └── Consts/              # 模块常量
│   │   └── dev/                         # 代码生成器
│   └── gateways/                        # 网关（单体模式未使用）
└── ui/
    └── zhontai.ui.admin.vue3/           # Vue3 前端
        └── src/
            ├── api/admin/               # 自动生成的API客户端
            └── views/business/          # 进销存业务页面
                ├── product/             # 商品管理 ✅
                ├── contact/             # 往来单位 ✅
                ├── warehouse/           # 仓库管理 ✅
                └── account/             # 结算账户 ✅
```

## 🎯 设计理念

相较精斗云的简化：
- ❌ 去掉订单/单据分离（购货订单+购货单 → 购货单）
- ❌ 去掉复杂审批流（保留单级审核/反审核）
- ❌ 去掉组装拆卸、序列号、批次保质期
- ❌ 去掉核销单（收款单/付款单直接核销）
- ✅ 保留核心：商品/客户供应商/购货/销货/库存/收付款/盘点/调拨

## 📄 License

MIT
