<template>
  <div class="my-flex-column w100 h100">
    <el-card class="my-query-box mt8" shadow="never">
      <el-form :model="state.input" :inline="true" @submit.stop.prevent>
        <el-form-item prop="keyword">
          <el-input v-model="state.input.keyword" placeholder="账户名称/编号" @keyup.enter="onQuery" clearable />
        </el-form-item>
        <el-form-item>
          <el-button auto-insert-space type="primary" icon="ele-Search" @click="onQuery">查询</el-button>
          <el-button auto-insert-space type="primary" icon="ele-Plus" @click="onAdd">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="my-fill mt8" shadow="never">
      <el-table v-loading="state.loading" :data="state.tableData" row-key="id" border stripe style="width:100%">
        <el-table-column prop="code" label="编号" width="100" />
        <el-table-column prop="name" label="账户名称" min-width="150" />
        <el-table-column prop="accountType" label="类型" width="90">
          <template #default="{ row }">
            <el-tag size="small">{{ row.accountType === 'Cash' ? '现金' : row.accountType === 'Bank' ? '银行' : row.accountType === 'Alipay' ? '支付宝' : row.accountType === 'Wechat' ? '微信' : row.accountType }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="initBalance" label="期初余额" width="120" align="right" />
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
    <account-form ref="accountFormRef" />
  </div>
</template>

<script lang="ts" setup name="business/account">
import { AccountEntity } from '/@/api/admin/data-contracts'
import { AccountApi } from '/@/api/admin/Account'
import eventBus from '/@/utils/mitt'
import { ElMessage, ElMessageBox } from 'element-plus'

const AccountForm = defineAsyncComponent(() => import('./components/account-form.vue'))
const accountFormRef = useTemplateRef('accountFormRef')
const state = reactive({
  loading: false, total: 0,
  input: { currentPage: 1, pageSize: 20, keyword: '' } as any,
  tableData: [] as AccountEntity[],
})
const onQuery = async () => {
  state.loading = true
  state.input.filter = { name: state.input.keyword }
  const res = await new AccountApi().getPage(state.input).catch(() => { state.loading = false })
  if (res?.data) { state.tableData = res.data.list ?? []; state.total = res.data.total ?? 0 }
  state.loading = false
}
onMounted(() => { eventBus.off('refreshAccount'); eventBus.on('refreshAccount', () => onQuery()); onQuery() })
onBeforeUnmount(() => { eventBus.off('refreshAccount') })
const onAdd = () => accountFormRef.value!.open({ isEnabled: true, accountType: 'Bank' })
const onEdit = (row: AccountEntity) => accountFormRef.value!.open(row)
const onDelete = async (row: AccountEntity) => {
  await ElMessageBox.confirm(`确定删除账户「${row.name}」吗？`, '提示', { type: 'warning' })
  const res = await new AccountApi().delete({ id: row.id })
  if (res?.success) { ElMessage.success('删除成功'); onQuery() }
}
</script>
