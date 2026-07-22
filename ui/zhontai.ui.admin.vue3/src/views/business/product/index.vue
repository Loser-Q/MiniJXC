<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item prop="keyword">
          <el-input v-model="state.input.keyword" placeholder="商品名称/编码/条码" @keyup.enter="onQuery" clearable />
        </el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
          <el-button auto-insert-space type="primary" icon="ele-Plus" @click="onAdd">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width: 100%"
        @sort-change="onSortChange" @selection-change="onSelectionChange">
        <el-table-column type="selection" width="55" />
        <el-table-column prop="code" label="商品编号" width="120" sortable="custom" show-overflow-tooltip />
        <el-table-column prop="name" label="商品名称" min-width="150" sortable="custom" show-overflow-tooltip />
        <el-table-column prop="category" label="类别" width="100" show-overflow-tooltip />
        <el-table-column prop="specification" label="规格型号" width="120" show-overflow-tooltip />
        <el-table-column prop="unit" label="单位" width="70" />
        <el-table-column prop="purchasePrice" label="采购价" width="100" align="right" />
        <el-table-column prop="salePrice" label="销售价" width="100" align="right" />
        <el-table-column prop="isEnabled" label="启用" width="70" align="center">
          <template #default="{ row }">
            <el-switch v-model="row.isEnabled" disabled />
          </template>
        </el-table-column>
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

    <product-form ref="productFormRef" />
  </div>
</template>

<script lang="ts" setup name="business/product">
import { PageInputProductEntity, ProductEntity } from '/@/api/admin/data-contracts'
import { ProductApi } from '/@/api/admin/Product'
import eventBus from '/@/utils/mitt'
import { ElMessage, ElMessageBox } from 'element-plus'

const ProductForm = defineAsyncComponent(() => import('./components/product-form.vue'))

const productFormRef = useTemplateRef('productFormRef')

const state = reactive({
  loading: false,
  total: 0,
  selectedRows: [] as ProductEntity[],
  input: {
    currentPage: 1,
    pageSize: 20,
    keyword: '',
    filter: {} as ProductEntity,
    dynamicFilter: {} as any,
  } as PageInputProductEntity & { keyword: string },
  tableData: [] as ProductEntity[],
})

const onQuery = async () => {
  state.loading = true
  state.input.filter = { name: state.input.keyword } as any
  const res = await new ProductApi().getPage(state.input as any).catch(() => { state.loading = false })
  if (res?.data) {
    state.tableData = res.data.list ?? []
    state.total = res.data.total ?? 0
  }
  state.loading = false
}

onMounted(() => {
  eventBus.off('refreshProduct')
  eventBus.on('refreshProduct', () => onQuery())
  onQuery()
})

onBeforeUnmount(() => { eventBus.off('refreshProduct') })

const onSortChange = (data: any) => { onQuery() }
const onSelectionChange = (rows: any) => { state.selectedRows = rows }

const onAdd = () => productFormRef.value!.open({ isEnabled: true })
const onEdit = (row: ProductEntity) => productFormRef.value!.open(row)

const onDelete = async (row: ProductEntity) => {
  await ElMessageBox.confirm(`确定删除商品「${row.name}」吗？`, '提示', { type: 'warning' })
  const res = await new ProductApi().delete({ id: row.id })
  if (res?.success) { ElMessage.success('删除成功'); onQuery() }
}
</script>
