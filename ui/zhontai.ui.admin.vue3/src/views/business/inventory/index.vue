<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item><el-input v-model="state.input.keyword" placeholder="商品名称" @keyup.enter="onQuery" clearable /></el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width:100%">
        <el-table-column prop="productName" label="商品" min-width="150" />
        <el-table-column prop="productCode" label="编号" width="100" />
        <el-table-column prop="warehouseName" label="仓库" width="120" />
        <el-table-column prop="billType" label="单据类型" width="100">
          <template #default="{ row }">
            <el-tag size="small" :type="row.billType==='Purchase'?'success':row.billType==='Sale'?'danger':'info'">{{ row.billType }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="billNo" label="单据编号" width="150" />
        <el-table-column prop="changeQty" label="变动数量" width="110" align="right" />
        <el-table-column prop="afterQty" label="结存" width="110" align="right" />
        <el-table-column prop="billDate" label="日期" width="110" />
      </el-table>
      <el-pagination class="mt15" v-model:current-page="state.input.currentPage" v-model:page-size="state.input.pageSize"
        :page-sizes="[20,50,100]" :total="state.total" layout="total,sizes,prev,pager,next,jumper" small @size-change="onQuery" @current-change="onQuery" background />
    </el-card>
  </div>
</template>

<script lang="ts" setup name="business/inventory">
import { InventoryEntity } from '/@/api/admin/data-contracts'
import { InventoryApi } from '/@/api/admin/Inventory'
import eventBus from '/@/utils/mitt'

const state = reactive({ loading: false, total: 0,
  input: { currentPage: 1, pageSize: 20, keyword: '' } as any,
  tableData: [] as any[] })
const onQuery = async () => {
  state.loading = true
  const res = await new InventoryApi().getPage({ currentPage: state.input.currentPage, pageSize: state.input.pageSize } as any).catch(() => { state.loading = false })
  if (res?.data) { state.tableData = res.data.list ?? []; state.total = res.data.total ?? 0 }
  state.loading = false
}
onMounted(() => { onQuery() })
</script>
