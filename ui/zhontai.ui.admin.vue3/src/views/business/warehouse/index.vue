<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item prop="keyword">
          <el-input v-model="state.input.keyword" placeholder="仓库名称/编号" @keyup.enter="onQuery" clearable />
        </el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
          <el-button auto-insert-space type="primary" icon="ele-Plus" @click="onAdd">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width:100%">
        <el-table-column prop="code" label="编号" width="120" />
        <el-table-column prop="name" label="仓库名称" min-width="150" />
        <el-table-column prop="address" label="地址" min-width="150" show-overflow-tooltip />
        <el-table-column label="操作" width="140" fixed="right" header-align="center" align="center">
          <template #default="{ row }">
            <el-button auto-insert-space icon="ele-EditPen" text type="primary" @click="onEdit(row)">编辑</el-button>
            <el-button auto-insert-space icon="ele-Delete" text type="danger" @click="onDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination class="mt15" v-model:current-page="state.input.currentPage" v-model:page-size="state.input.pageSize"
        :page-sizes="[20, 50, 100]" :total="state.total" layout="total, sizes, prev, pager, next, jumper" small
        @size-change="onQuery" @current-change="onQuery" background />
    </el-card>
    <warehouse-form ref="warehouseFormRef" />
  </div>
</template>

<script lang="ts" setup name="business/warehouse">
import { WarehouseEntity } from '/@/api/admin/data-contracts'
import { WarehouseApi } from '/@/api/admin/Warehouse'
import eventBus from '/@/utils/mitt'
import { ElMessage, ElMessageBox } from 'element-plus'

const WarehouseForm = defineAsyncComponent(() => import('./components/warehouse-form.vue'))
const warehouseFormRef = useTemplateRef('warehouseFormRef')
const state = reactive({
  loading: false, total: 0,
  input: { currentPage: 1, pageSize: 20, keyword: '' } as any,
  tableData: [] as WarehouseEntity[],
})

const onQuery = async () => {
  state.loading = true
  state.input.filter = { name: state.input.keyword }
  const res = await new WarehouseApi().getPage(state.input).catch(() => { state.loading = false })
  if (res?.data) { state.tableData = res.data.list ?? []; state.total = res.data.total ?? 0 }
  state.loading = false
}
onMounted(() => { eventBus.off('refreshWarehouse'); eventBus.on('refreshWarehouse', () => onQuery()); onQuery() })
onBeforeUnmount(() => { eventBus.off('refreshWarehouse') })
const onAdd = () => warehouseFormRef.value!.open({ isEnabled: true })
const onEdit = (row: WarehouseEntity) => warehouseFormRef.value!.open(row)
const onDelete = async (row: WarehouseEntity) => {
  await ElMessageBox.confirm(`确定删除仓库「${row.name}」吗？`, '提示', { type: 'warning' })
  const res = await new WarehouseApi().delete({ id: row.id })
  if (res?.success) { ElMessage.success('删除成功'); onQuery() }
}
</script>
