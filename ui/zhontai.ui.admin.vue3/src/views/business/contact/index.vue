<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item prop="keyword">
          <el-input v-model="state.input.keyword" placeholder="单位名称/编号" @keyup.enter="onQuery" clearable />
        </el-form-item>
        <el-form-item>
          <el-select v-model="state.input.filter.type" placeholder="类型" clearable style="width:120px" @change="onQuery">
            <el-option label="全部" value="" />
            <el-option label="客户" value="Customer" />
            <el-option label="供应商" value="Supplier" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
          <el-button auto-insert-space type="primary" icon="ele-Plus" @click="onAdd">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width: 100%">
        <el-table-column prop="code" label="编号" width="100" show-overflow-tooltip />
        <el-table-column prop="name" label="名称" min-width="150" show-overflow-tooltip />
        <el-table-column prop="type" label="类型" width="90">
          <template #default="{ row }">
            <el-tag :type="row.type === 'Customer' ? 'success' : 'warning'" size="small">
              {{ row.type === 'Customer' ? '客户' : '供应商' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="contactPerson" label="联系人" width="100" show-overflow-tooltip />
        <el-table-column prop="phone" label="电话" width="120" />
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
    <contact-form ref="contactFormRef" />
  </div>
</template>

<script lang="ts" setup name="business/contact">
import { PageInputContactEntity, ContactEntity } from '/@/api/admin/data-contracts'
import { ContactApi } from '/@/api/admin/Contact'
import eventBus from '/@/utils/mitt'
import { ElMessage, ElMessageBox } from 'element-plus'

const ContactForm = defineAsyncComponent(() => import('./components/contact-form.vue'))
const contactFormRef = useTemplateRef('contactFormRef')

const state = reactive({
  loading: false, total: 0,
  input: { currentPage: 1, pageSize: 20, keyword: '', filter: { type: '' } as any } as any,
  tableData: [] as ContactEntity[],
})

const onQuery = async () => {
  state.loading = true
  const res = await new ContactApi().getPage(state.input).catch(() => { state.loading = false })
  if (res?.data) { state.tableData = res.data.list ?? []; state.total = res.data.total ?? 0 }
  state.loading = false
}

onMounted(() => {
  eventBus.off('refreshContact'); eventBus.on('refreshContact', () => onQuery()); onQuery()
})
onBeforeUnmount(() => { eventBus.off('refreshContact') })

const onAdd = () => contactFormRef.value!.open({ isEnabled: true, type: 'Customer' })
const onEdit = (row: ContactEntity) => contactFormRef.value!.open(row)
const onDelete = async (row: ContactEntity) => {
  await ElMessageBox.confirm(`确定删除「${row.name}」吗？`, '提示', { type: 'warning' })
  const res = await new ContactApi().delete({ id: row.id })
  if (res?.success) { ElMessage.success('删除成功'); onQuery() }
}
</script>
