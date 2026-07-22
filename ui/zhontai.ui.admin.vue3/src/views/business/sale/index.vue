<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item><el-input v-model="state.input.keyword" placeholder="单据编号" @keyup.enter="onQuery" clearable /></el-form-item>
        <el-form-item><el-date-picker v-model="state.input.dateRange" type="daterange" range-separator="至" start-placeholder="开始日期" end-placeholder="结束日期" value-format="YYYY-MM-DD" /></el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
          <el-button auto-insert-space type="primary" icon="ele-Plus" @click="onAdd">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width:100%">
        <el-table-column prop="billNo" label="单据编号" width="150" />
        <el-table-column prop="billDate" label="日期" width="110" />
        <el-table-column prop="customerId" label="客户" width="150" />
        <el-table-column prop="receivedAmount" label="收款金额" width="120" align="right" />
        <el-table-column prop="owedAmount" label="欠款" width="120" align="right" />
        <el-table-column prop="isAudited" label="审核" width="80" align="center">
          <template #default="{ row }"><el-tag :type="row.isAudited?'success':'info'" size="small">{{ row.isAudited?'已审':'未审' }}</el-tag></template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right" header-align="center" align="center">
          <template #default="{ row }">
            <el-button auto-insert-space icon="ele-EditPen" text type="primary" @click="onEdit(row)">编辑</el-button>
            <el-button v-if="!row.isAudited" auto-insert-space icon="ele-Check" text type="success" @click="onAudit(row)">审核</el-button>
            <el-button auto-insert-space icon="ele-Delete" text type="danger" @click="onDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination class="mt15" v-model:current-page="state.input.currentPage" v-model:page-size="state.input.pageSize"
        :page-sizes="[20,50,100]" :total="state.total" layout="total,sizes,prev,pager,next,jumper" small @size-change="onQuery" @current-change="onQuery" background />
    </el-card>
    <sale-form ref="saleFormRef" />
  </div>
</template>

<script lang="ts" setup name="business/sale">
import { SaleEntity } from '/@/api/admin/data-contracts'
import { SaleApi } from '/@/api/admin/Sale'
import eventBus from '/@/utils/mitt'
import { ElMessage, ElMessageBox } from 'element-plus'

const SaleForm = defineAsyncComponent(() => import('./components/sale-form.vue'))
const saleFormRef = useTemplateRef('saleFormRef')
const state = reactive({ loading: false, total: 0,
  input: { currentPage: 1, pageSize: 20, keyword: '', dateRange: [] as string[] } as any,
  tableData: [] as SaleEntity[] })

const onQuery = async () => {
  state.loading = true
  const res = await new SaleApi().getPage({ currentPage: state.input.currentPage, pageSize: state.input.pageSize } as any).catch(() => { state.loading = false })
  if (res?.data) { state.tableData = res.data.list ?? []; state.total = res.data.total ?? 0 }
  state.loading = false
}
onMounted(() => { eventBus.off('refreshSale'); eventBus.on('refreshSale', () => onQuery()); onQuery() })
onBeforeUnmount(() => { eventBus.off('refreshSale') })
const onAdd = () => saleFormRef.value!.open({ billDate: new Date().toISOString().split('T')[0], items: [] })
const onEdit = (row: SaleEntity) => saleFormRef.value!.open(row)
const onAudit = async (row: SaleEntity) => {
  await ElMessageBox.confirm(`确定审核销货单「${row.billNo}」吗？`, '提示', { type: 'warning' })
  const res = await new SaleApi().audit({ id: row.id })
  if (res?.success) { ElMessage.success('审核成功'); onQuery() }
}
const onDelete = async (row: SaleEntity) => {
  await ElMessageBox.confirm(`确定删除「${row.billNo}」吗？`, '提示', { type: 'warning' })
  const res = await new SaleApi().delete({ id: row.id })
  if (res?.success) { ElMessage.success('删除成功'); onQuery() }
}
</script>
