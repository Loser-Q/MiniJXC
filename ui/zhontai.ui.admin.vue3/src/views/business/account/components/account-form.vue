<template>
  <div>
    <el-dialog v-model="state.showDialog" destroy-on-close :title="state.form.id > 0 ? '编辑账户' : '新增账户'" draggable
      :close-on-click-modal="false" width="500px">
      <el-form ref="formRef" :model="state.form" label-width="80px">
        <el-row :gutter="20">
          <el-col :xs="24" :sm="12">
            <el-form-item label="编号" prop="code" :rules="[{ required: true, message: '请输入编号' }]">
              <el-input v-model="state.form.code" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="名称" prop="name" :rules="[{ required: true, message: '请输入名称' }]">
              <el-input v-model="state.form.name" />
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="类型">
              <el-select v-model="state.form.accountType" style="width:100%">
                <el-option label="现金" value="Cash" />
                <el-option label="银行" value="Bank" />
                <el-option label="支付宝" value="Alipay" />
                <el-option label="微信" value="Wechat" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="期初余额"><el-input-number v-model="state.form.initBalance" :precision="2" :min="0" style="width:100%" /></el-form-item>
          </el-col>
          <el-col :xs="24" :sm="12">
            <el-form-item label="启用"><el-switch v-model="state.form.isEnabled" /></el-form-item>
          </el-col>
          <el-col :xs="24">
            <el-form-item label="备注"><el-input v-model="state.form.remark" type="textarea" /></el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="state.showDialog = false">取消</el-button>
        <el-button type="primary" @click="onSure" :loading="state.sureLoading">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="business/account/form">
import { AccountEntity } from '/@/api/admin/data-contracts'
import { AccountApi } from '/@/api/admin/Account'
import eventBus from '/@/utils/mitt'
import { FormInstance, ElMessage } from 'element-plus'

const formRef = useTemplateRef<FormInstance>('formRef')
const state = reactive({ showDialog: false, sureLoading: false, form: {} as AccountEntity })
const open = async (row: any = {}) => {
  if (row.id > 0) { const res = await new AccountApi().get({ id: row.id }); if (res?.data) state.form = res.data as any }
  else { state.form = { ...row } as any }
  state.showDialog = true
}
const onSure = () => {
  formRef.value!.validate(async (valid: boolean) => {
    if (!valid) return; state.sureLoading = true
    let res: any
    if ((state.form as any).id > 0) res = await new AccountApi().update(state.form as any).catch(() => { state.sureLoading = false })
    else res = await new AccountApi().add(state.form as any).catch(() => { state.sureLoading = false })
    state.sureLoading = false
    if (res?.success) { ElMessage.success('成功'); eventBus.emit('refreshAccount'); state.showDialog = false }
  })
}
defineExpose({ open })
</script>
